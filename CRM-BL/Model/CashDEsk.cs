using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_BL.Model
{
    /// <summary>
    /// Касса.
    /// </summary>
    public class CashDesk
    {
        CrmContext db = new CrmContext();

        public Seller Seller { get; set; }

        public Queue<Cart> Carts { get; set; }

        public int MaxQueueLenght { get; set; }
        /// <summary>
        /// Покупатель ушедший из магазина если большая очередь.
        /// </summary>
        public int ExitCustomer { get; set; }

        /// <summary>
        /// Номер кассы.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Флаг сохранения в БД.
        /// </summary>
        public bool IsModel { get; set; }

        public int Count => Carts.Count;

        /// <summary>
        /// Закрытие чека.
        /// </summary>
        public EventHandler<Check> CheckClosed;
        /// <summary>
        /// Создать кассу.
        /// </summary>
        /// <param name="number">Номер кассы.</param>
        /// <param name="seller">Продавец.</param>
        public CashDesk(int number, Seller seller)
        {
            Seller = seller;
            Number = number;
            Carts = new Queue<Cart>();
            IsModel = true;
            MaxQueueLenght = 10;
        }

        /// <summary>
        /// Добавить в очередь корзину.
        /// </summary>
        /// <param name="cart">Корзина.</param>
        public void Enqueue(Cart cart)
        {
            if (Carts.Count < MaxQueueLenght)
            {
                Carts.Enqueue(cart);
            }
            else
            {
                ExitCustomer++;
            }
        }

        /// <summary>
        /// Извлечь корзину из очереди.
        /// </summary>
        /// <returns>Сумма.</returns>
        public decimal Dequeue()
        {
            decimal sum = 0;
            if(Carts.Count == 0)
            {
                return 0;
            }
            var cart = Carts.Dequeue();
            if (cart != null)
            {
                var check = new Check()
                {
                    SellerId = this.Seller.SellerId,
                    Seller = this.Seller,
                    CustomerId = cart.Customer.CustomerId,
                    Customer = cart.Customer,
                    Created = DateTime.Now
                };

                if (!IsModel)
                {
                    db.Checks.Add(check);
                    db.SaveChanges();
                }
                else
                {
                    check.CheckId = 0;
                }

                var sells = new List<Sell>();


                foreach (Product product in cart)
                {
                    if (product.Count > 0)
                    {
                        var sell = new Sell()
                        {
                            CheckId = check.CheckId,
                            Check = check,
                            ProductId = product.ProductId,
                            Product = product
                        };

                        sells.Add(sell);

                        if (!IsModel)
                        {
                            db.Sells.Add(sell);
                        }

                        product.Count--;
                        sum += product.Price;
                    }
                }
                check.Price = sum;

                if (!IsModel)
                {
                    db.SaveChanges();
                }

                CheckClosed?.Invoke(this, check);
            }
            return sum;
        }




        public override string ToString()
        {
            return $"Касса № {Number}";
        }
    }
}
