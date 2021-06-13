using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CRM_BL.Model
{
    public class ShopComputerModel
    {
        Generator Generator = new Generator();
        Random random = new Random();

        public List<Check> Checks { get; set; } = new List<Check>();
        public List<CashDesk> CashDesks { get; set; } = new List<CashDesk>();
        public List<Cart> Carts { get; set; } = new List<Cart>();
        public List<Sell> Sells { get; set; } = new List<Sell>();
        public Queue<Seller> Sellers { get; set; } = new Queue<Seller>();
        private bool isWorking = false;

        public int CashDeskSpeed { get; set; } = 100;
        public int CustomerSpeed { get; set; } = 100;

        public ShopComputerModel()
        {

            var sellers = Generator.GetNewSellers(10);
            Generator.GetNewProducts(1000);
            Generator.GetNewCustomers(100);

            foreach (var seller in sellers)
            {
                Sellers.Enqueue(seller);
            }

            for (int i = 0; i < 3; i++)
            {
                CashDesks.Add(new CashDesk(CashDesks.Count, Sellers.Dequeue()));
            }
        }

        public void Start()
        {
            isWorking = true;
            Task.Run(() => CreateNewCarts(10));
            var cashDeskTasks = CashDesks.Select(c => new Task(() => CashDeskWork(c)));
            foreach (var task in cashDeskTasks)
            {
                task.Start();
            }

        }

        public void Stop()
        {
            isWorking = false;
        }

        private void CashDeskWork(CashDesk cashDesk)
        {
            if (cashDesk.Count > 0)
            {
                cashDesk.Dequeue();
            }
            Thread.Sleep(CashDeskSpeed);
        }

        private void CreateNewCarts(int customerCount)
        {
            while (isWorking)
            {
                var customers = Generator.GetNewCustomers(customerCount);

                foreach (var customer in customers)
                {
                    var cart = new Cart(customer);

                    foreach (var product in Generator.GetRandomProduct(10, 30))
                    {
                        cart.Add(product);
                    }

                    var cash = CashDesks[random.Next(CashDesks.Count)];
                    cash.Enqueue(cart);
                }
                Thread.Sleep(CustomerSpeed);
            }
        }
    }
}
