using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_BL.Model
{
    /// <summary>
    /// Генератор модельных классов
    /// </summary>
    class Generator
    {
        Random random = new Random();

        public List<Customer> Customers { get; set; } = new List<Customer>();
        public List<Product> Products { get; set; } = new List<Product>();
        public List<Seller> Sellers { get; set; } = new List<Seller>();

        /// <summary>
        /// Получить новый список продуктов.
        /// </summary>
        /// <param name="count">Кол-во продуктов.</param>
        /// <returns></returns>
        public List<Product> GetNewProducts(int count)
        {
            List<Product> result = new List<Product>();
            for (int i = 0; i < count; i++)
            {
                var product = new Product()
                {
                    ProductId = Products.Count,
                    Name = GetRandomText(),
                    Count = random.Next(20, 1000),
                    Price = (decimal)(random.Next(5, 10000) + random.NextDouble())
                };
                Products.Add(product);
                result.Add(product);
            }
            return result;
        }

        public List<Customer> GetNewCustomers(int count)
        {
            List<Customer> result = new List<Customer>();
            for (int i = 0; i < count; i++)
            {
                var customer = new Customer()
                {
                    CustomerId = Customers.Count,
                    Name = GetRandomText()                   
                };
                Customers.Add(customer);
                result.Add(customer);
            }
            return result;
        }

        public List<Seller> GetNewSellers(int count)
        {
            List<Seller> result = new List<Seller>();
            for (int i = 0; i < count; i++)
            {
                var seller = new Seller()
                {
                    SellerId = Sellers.Count,
                    Name = GetRandomText()
                };
                Sellers.Add(seller);
                result.Add(seller);
            }
            return result;
        }

        public List<Product> GetRandomProduct(int min, int max)
        {
            var result = new List<Product>();
            var count = random.Next(min, max);
            for (int i = 0; i < count; i++)
            {
                result.Add(Products[random.Next(Products.Count - 1)]);
            }

            return result;
        }


        private static string GetRandomText()
        {
            return "name " + Guid.NewGuid().ToString().Substring(0, 5);
        }
    }
}
