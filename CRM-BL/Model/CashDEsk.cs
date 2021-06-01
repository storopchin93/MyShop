using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_BL.Model
{
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

        public int Number { get; set; }

        /// <summary>
        /// Флаг сохранения в БД.
        /// </summary>
        public bool IsModel { get; set; }

        public CashDesk(int number, Seller seller)
        {
            Seller = seller;
            Number = number;
            Carts = new Queue<Cart>();
            IsModel = true;
        }

        public void Enqueue(Cart cart)
        {
            if (Carts.Count <= MaxQueueLenght)
            {
                Carts.Enqueue(cart);
            }
            else
            {
                ExitCustomer++;
            }
        }

        public decimal Dequeue()
        {
            var card = Carts.Dequeue();
            decimal sum = 0;
            if (card != null)
            {
                var check = new Check()
                {
                    SellerId = this.Seller.SellerId,
                    Seller = this.Seller,
                    CustomerId = card.Customer.CustomerId,
                    Customer = card.Customer,
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


                foreach (Product product in card)
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

                if (!IsModel)
                {
                    db.SaveChanges();
                }
            }
            return sum;
        }
    }
}
