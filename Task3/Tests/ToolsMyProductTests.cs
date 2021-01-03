﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Task3;

namespace Tests
{
    [TestClass]
    public class ToolsMyProductTests
    {
        [TestMethod]
        public void MyProductGetProductsByNameTest()
        {
            DataBaseDataContext db = new DataBaseDataContext();
            List<MyProduct> queryFromMethod = ToolsMyProduct.MyProductGetProductsByName("Crankarm");
            queryFromMethod.Sort((x, y) => x.ProductID.CompareTo(y.ProductID));

            List<MyProduct> queryFromDB = new List<MyProduct>();
            MyProduct myProduct = (from p in db.MyProduct where p.ProductID == 317 select p).First();
            queryFromDB.Add(myProduct);
            myProduct = (from p in db.MyProduct where p.ProductID == 318 select p).First();
            queryFromDB.Add(myProduct);
            myProduct = (from p in db.MyProduct where p.ProductID == 319 select p).First();
            queryFromDB.Add(myProduct);

            for (int i = 0; i < queryFromMethod.Count(); i++)
            {
                Assert.AreEqual(queryFromDB[i].ProductID, queryFromMethod[i].ProductID);
                Assert.AreEqual(queryFromDB[i].Name, queryFromMethod[i].Name);
            }
            Assert.AreEqual(queryFromDB.Count(), queryFromMethod.Count());
        }

        [TestMethod]
        public void MyProductGetProductsByVendorNameTest()
        {
            DataBaseDataContext db = new DataBaseDataContext();

            List<MyProduct> queryFromMethod = ToolsMyProduct.MyProductGetProductsByVendorName("Greenwood Athletic Company");

            List<MyProduct> queryFromDB = new List<MyProduct>();
            MyProduct myProduct = (from p in db.MyProduct where p.ProductID == 935 select p).First();
            queryFromDB.Add(myProduct);
            myProduct = (from p in db.MyProduct where p.ProductID == 936 select p).First();
            queryFromDB.Add(myProduct);

            for (int i = 0; i < queryFromMethod.Count(); i++)
            {
                Assert.AreEqual(queryFromDB[i].ProductID, queryFromMethod[i].ProductID);
                Assert.AreEqual(queryFromDB[i].Name, queryFromMethod[i].Name);
            }
            Assert.AreEqual(queryFromDB.Count(), queryFromMethod.Count());
        }

        [TestMethod]
        public void MyProductGetNRecentlyReviewedProductsTest()
        {
            DataBaseDataContext db = new DataBaseDataContext();

            List<MyProduct> queryFromMethod = ToolsMyProduct.MyProductGetNRecentlyReviewedProducts(5);

            List<MyProduct> queryFromDB = new List<MyProduct>();
            MyProduct myProduct = (from p in db.MyProduct where p.ProductID == 709 select p).First();
            queryFromDB.Add(myProduct);
            myProduct = (from p in db.MyProduct where p.ProductID == 937 select p).First();
            queryFromDB.Add(myProduct);
            queryFromDB.Add(myProduct);
            myProduct = (from p in db.MyProduct where p.ProductID == 798 select p).First();
            queryFromDB.Add(myProduct);

            for (int i = 0; i < queryFromMethod.Count(); i++)
            {
                Assert.AreEqual(queryFromDB[i].ProductID, queryFromMethod[i].ProductID);
                Assert.AreEqual(queryFromDB[i].Name, queryFromMethod[i].Name);
            }
            Assert.AreEqual(queryFromDB.Count(), queryFromMethod.Count());
        }
    }
}
