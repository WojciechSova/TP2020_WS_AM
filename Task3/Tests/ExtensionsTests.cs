using System;
using Task3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class ExtensionsTests
    {
        [TestMethod]+
        public void WithoutCategoryMethodTest()
        {
            DataBaseDataContext db = new DataBaseDataContext();
            List<Product> query = (from p in db.Products select p).ToList();
            query = query.WithoutCategoryMethod();

            Assert.AreEqual(209, query.Count());
        }

        [TestMethod]
        public void WithoutCategoryQueryTest()
        {
            DataBaseDataContext db = new DataBaseDataContext();
            List<Product> query = (from p in db.Products select p).ToList();
            query = query.WithoutCategoryQuery();

            Assert.AreEqual(209, query.Count());
        }

        [TestMethod()]
        public void SplitIntoPagesTest()
        {
            DataBaseDataContext db = new DataBaseDataContext();
            List<Product> query = (from p in db.Products select p).ToList();
            query.Sort((x, y) => x.ProductID.CompareTo(y.ProductID));
            query = query.SplitIntoPages(3, 4);

            List<Product> tmp = new List<Product>();
            Product C = (from p in db.Products where p.ProductID == 324 select p).First();
            tmp.Add(C);
            C = (from p in db.Products where p.ProductID == 325 select p).First();
            tmp.Add(C);
            C = (from p in db.Products where p.ProductID == 326 select p).First();
            tmp.Add(C);

            for (int i = 0; i < query.Count(); i++)
            {
                Assert.AreEqual(tmp[i].ProductID, query[i].ProductID);
            }
            Assert.AreEqual(tmp.Count, query.Count);

        }

        [TestMethod()]
        public void GetProductAndNameTests()
        {
            DataBaseDataContext db = new DataBaseDataContext();
            List<Product> all = (from p in db.Products select p).ToList();
            List<Product> query = Tools.GetProductsByName("Thin-Jam Hex Nut");
            query.Sort((x, y) => x.ProductID.CompareTo(y.ProductID));

            List<String> productsAndNames = query.GetProductAndName().Split('\n').ToList();

            for (int i = 0; i < query.Count; i++)
            {
                Assert.AreEqual(all.Find(x => x.ProductID == 359 + i).Name + " - " + "Advanced Bicycles", productsAndNames[i]);
            }
        }
    }


}
