using Microsoft.VisualStudio.TestTools.UnitTesting;
using CRM_BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_BL.Model.Tests
{
    [TestClass()]
    public class CashDeskTests
    {
        [TestMethod()]
        public void CashDeskTest()
        {
            //avarrage
            Customer customer = new Customer()
            {
                Name = "testCustomer",
                CustomerId = 1
            };

            Customer customer2 = new Customer()
            {
                Name = "testCustomer2",
                CustomerId = 2
            };

            Seller seller = new Seller()
            {
                Name = "testSeller",
                SellerId = 1,
            };

            var product1 = new Product()
            {
                ProductId = 1,
                Name = "pr1",
                Price = 100,
                Count = 10
            };
            var product2 = new Product()
            {
                ProductId = 2,
                Name = "prod2",
                Price = 200,
                Count = 20
            };


            Cart cart = new Cart(customer);

            cart.Add(product2);
            cart.Add(product1);
            cart.Add(product2);

            Cart cart2 = new Cart(customer2);

            cart2.Add(product2);
            cart2.Add(product2);
            cart2.Add(product2);

            var cart1ExpectedResult = 500;
            var cart2ExpectedResult = 600;

            //act 
            CashDesk cashDesk = new CashDesk(1, seller);
            cashDesk.MaxQueueLenght = 10;
            cashDesk.Enqueue(cart);
            cashDesk.Enqueue(cart2);

            var cart1ActualResult = cashDesk.Dequeue();
            var cart2ActualResult = cashDesk.Dequeue();

            //assert

            Assert.AreEqual(cart1ExpectedResult, cart1ActualResult);
            Assert.AreEqual(cart2ExpectedResult, cart2ActualResult);
            Assert.AreEqual(15, product2.Count);
            Assert.AreEqual(9, product1.Count);

        }
    }
}