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
            List<Product> zapytanie = Tools.GetProductsByName("Flat");

            List<Product> stala = new List<Product>();
            Product C = (from p in db.Products where p.ProductID == 325 select p).First();
            stala.Add(C);
            C = (from p in db.Products where p.ProductID == 326 select p).First();
            stala.Add(C);

            for (int i = 0; i < zapytanie.Count(); i++)
            {
                if (zapytanie[i].ProductID != stala[i].ProductID) Assert.Fail();
                if (zapytanie[i].Name != stala[i].Name) Assert.Fail();
            }
            if (zapytanie.Count() != stala.Count()) Assert.Fail();
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
