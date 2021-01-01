using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Task3;

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
            DataBaseDataContext db = new DataBaseDataContext();

            List<Product> queryFromMethod = Tools.GetProductsByVendorName("Greenwood Athletic Company");

            List<Product> queryFromDB = new List<Product>();
            Product product = (from p in db.Products where p.ProductID == 935 select p).First();
            queryFromDB.Add(product);
            product = (from p in db.Products where p.ProductID == 936 select p).First();
            queryFromDB.Add(product);

            for (int i = 0; i < queryFromMethod.Count(); i++)
            {
                Assert.AreEqual(queryFromDB[i].ProductID, queryFromMethod[i].ProductID);
                Assert.AreEqual(queryFromDB[i].Name, queryFromMethod[i].Name);
            }
            Assert.AreEqual(queryFromDB.Count(), queryFromMethod.Count());
        }

        [TestMethod]
        public void GetProductNamesByVendorNameTest()
        {
            DataBaseDataContext db = new DataBaseDataContext();

            List<string> queryFromMethod = Tools.GetProductNamesByVendorName("Greenwood Athletic Company");

            List<Product> queryFromDB = new List<Product>();
            Product product = (from p in db.Products where p.ProductID == 935 select p).First();
            queryFromDB.Add(product);
            product = (from p in db.Products where p.ProductID == 936 select p).First();
            queryFromDB.Add(product);

            for (int i = 0; i < queryFromMethod.Count(); i++)
            {
                Assert.AreEqual(queryFromDB[i].Name, queryFromMethod[i]);
            }
            Assert.AreEqual(queryFromDB.Count(), queryFromMethod.Count());
        }

        [TestMethod]
        public void GetProductVendorByProductNameTest()
        {
            string query = Tools.GetProductVendorByProductName("Racing Socks, M");

            string temp = "Jeff's Sporting Goods";
            Assert.AreEqual(query, temp);
        }

        [TestMethod]
        public void GetProductsWithNRecentReviewsTest()
        {
            DataBaseDataContext db = new DataBaseDataContext();

            List<Product> queryFromMethod = Tools.GetProductsWithNRecentReviews(5);

            List<Product> queryFromDB = new List<Product>();
            Product product = (from p in db.Products where p.ProductID == 709 select p).First();
            queryFromDB.Add(product);
            product = (from p in db.Products where p.ProductID == 937 select p).First();
            queryFromDB.Add(product);
            queryFromDB.Add(product);
            product = (from p in db.Products where p.ProductID == 798 select p).First();
            queryFromDB.Add(product);

            for (int i = 0; i < queryFromMethod.Count(); i++)
            {
                Assert.AreEqual(queryFromDB[i].ProductID, queryFromMethod[i].ProductID);
                Assert.AreEqual(queryFromDB[i].Name, queryFromMethod[i].Name);
            }
            Assert.AreEqual(queryFromDB.Count(), queryFromMethod.Count());
        }

        [TestMethod]
        public void GetNRecentlyReviewedProductsTest()
        {
            DataBaseDataContext db = new DataBaseDataContext();

            List<Product> queryFromMethod = Tools.GetNRecentlyReviewedProducts(5);

            List<Product> queryFromDB = new List<Product>();
            Product product = (from p in db.Products where p.ProductID == 709 select p).First();
            queryFromDB.Add(product);
            product = (from p in db.Products where p.ProductID == 937 select p).First();
            queryFromDB.Add(product);
            queryFromDB.Add(product);
            product = (from p in db.Products where p.ProductID == 798 select p).First();
            queryFromDB.Add(product);

            for (int i = 0; i < queryFromMethod.Count(); i++)
            {
                Assert.AreEqual(queryFromDB[i].ProductID, queryFromMethod[i].ProductID);
                Assert.AreEqual(queryFromDB[i].Name, queryFromMethod[i].Name);
            }
            Assert.AreEqual(queryFromDB.Count(), queryFromMethod.Count());
        }

        [TestMethod]
        public void GetNProductsFromCategoryTest()
        {
            DataBaseDataContext db = new DataBaseDataContext();

            List<Product> queryFromMethod = Tools.GetNProductsFromCategory("Components", 2);

            List<Product> queryFromDB = new List<Product>();
            Product product = (from p in db.Products where p.ProductID == 808 select p).First();
            queryFromDB.Add(product);
            product = (from p in db.Products where p.ProductID == 809 select p).First();
            queryFromDB.Add(product);


            for (int i = 0; i < queryFromMethod.Count(); i++)
            {
                Assert.AreEqual(queryFromDB[i].ProductID, queryFromMethod[i].ProductID);
                Assert.AreEqual(queryFromDB[i].Name, queryFromMethod[i].Name);
            }
            Assert.AreEqual(queryFromDB.Count(), queryFromMethod.Count());
        }

        [TestMethod]
        public void GetTotalStandardCostByCategoryTest()
        {
            DataBaseDataContext db = new DataBaseDataContext();
            ProductCategory productCategory = (from p in db.ProductCategory where p.ProductCategoryID == 2 select p).First();

            double queryFromMethod = Math.Round(Tools.GetTotalStandardCostByCategory(productCategory), 2);
            double temp = 62961.28;
            Assert.AreEqual(queryFromMethod, temp);
        }




        [TestMethod]
        public void MyProductGetProductsByNameTest()
        {
            DataBaseDataContext db = new DataBaseDataContext();
            List<MyProduct> queryFromMethod = Tools.MyProductGetProductsByName("Crankarm");

            List<MyProduct> queryFromDB = new List<MyProduct>();
            MyProduct myProduct = (from p in db.MyProduct where p.ProductID == 319 select p).First();
            queryFromDB.Add(myProduct);
            myProduct = (from p in db.MyProduct where p.ProductID == 317 select p).First();
            queryFromDB.Add(myProduct);
            myProduct = (from p in db.MyProduct where p.ProductID == 318 select p).First();
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

            List<MyProduct> queryFromMethod = Tools.MyProductGetProductsByVendorName("Greenwood Athletic Company");

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

            List<MyProduct> queryFromMethod = Tools.MyProductGetNRecentlyReviewedProducts(5);

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
