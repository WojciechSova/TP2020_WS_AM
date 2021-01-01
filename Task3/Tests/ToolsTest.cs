using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class ToolsTest
    {
        [TestMethod]
        public void GetProductsByNameTest()
        {
            DataBaseDataContext db = new DataBaseDataContext();
            List<Product> queryFromMethod = Tools.GetProductsByName("Crankarm");

            List<Product> queryFromDB = new List<Product>();
            Product product = (from p in db.Products where p.ProductID == 319 select p).First();
            queryFromDB.Add(product);
            product = (from p in db.Products where p.ProductID == 317 select p).First();
            queryFromDB.Add(product);
            product = (from p in db.Products where p.ProductID == 318 select p).First();
            queryFromDB.Add(product);

            for (int i = 0; i < queryFromMethod.Count(); i++)
            {
                Assert.AreEqual(queryFromDB[i].ProductID, queryFromMethod[i].ProductID);
                Assert.AreEqual(queryFromDB[i].Name, queryFromMethod[i].Name);
            }
            Assert.AreEqual(queryFromDB.Count(), queryFromMethod.Count());
        }

        [TestMethod]
        public void GetProductsByVendorNameTest()
        {
        }

        [TestMethod]
        public void GetProductNamesByVendorNameTest()
        {
        }

        [TestMethod]
        public void GetProductVendorByProductNameTest()
        {
        }

        [TestMethod]
        public void GetProductsWithNRecentReviewsTest()
        {
        }

        [TestMethod]
        public void GetNRecentlyReviewedProductsTest()
        {
        }

        [TestMethod]
        public void GetNProductsFromCategoryTest()
        {
        }

        [TestMethod]
        public void GetTotalStandardCostByCategoryTest()
        {
        }




        [TestMethod]
        public void MyProductGetProductsByNameTest()
        {
        }

        [TestMethod]
        public void MyProductGetProductsByVendorNameTest()
        {
        }

        [TestMethod]
        public void MyProductGetNRecentlyReviewedProductsTest()
        {
        }
    }
}
