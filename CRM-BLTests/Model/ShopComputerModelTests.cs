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
    public class ShopComputerModelTests
    {
        [TestMethod()]
        public void StartTest()
        {
            ShopComputerModel shopComputerModel = new ShopComputerModel();
            shopComputerModel.Start();
        }
    }
}