using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyBuyList.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBuyList.DataLayer.Tests
{
    [TestClass()]
    public class DataFacadeTests
    {
        [TestMethod()]
        public void GetCategoriesListTest()
        {
            var list = DataFacade.Instance.GetCategoriesList();
            Assert.IsNotNull(list);
            //Assert.Fail();
        }
    }
}