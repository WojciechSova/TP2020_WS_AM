using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Linq;

namespace Task3
{
    public class ToolsProduct : IDisposable
    {
        private DataBaseDataContext db = null;
        public ToolsProduct()
        {
            db = new DataBaseDataContext();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public List<Product> GetProductsByName(string namePart)
        {
            List<Product> query = (from p in db.Products
                    where SqlMethods.Like(p.Name, "%" + namePart + "%")
                    select p)
                    .ToList();

            return query;
        }

        public List<Product> GetProductsByVendorName(string vendorName)
        {
            List<Product> query = (from p in db.Products
                                   join pv in db.ProductVendors on p.ProductID equals pv.ProductID
                                   join v in db.Vendors on pv.BusinessEntityID equals v.BusinessEntityID
                                   where SqlMethods.Like(v.Name, vendorName)
                                   select p).ToList();

            return query;
        }

        public List<string> GetProductNamesByVendorName(string vendorName)
        {
            List<string> query = (from p in db.Products
                                  join pv in db.ProductVendors on p.ProductID equals pv.ProductID
                                  join v in db.Vendors on pv.BusinessEntityID equals v.BusinessEntityID
                                  where SqlMethods.Like(v.Name, vendorName)
                                  select p.Name).ToList();

            return query;
        }
        public string GetProductVendorByProductName(string productName)
        {
            string query= (from p in db.Products
                             join pv in db.ProductVendors on p.ProductID equals pv.ProductID
                             join v in db.Vendors on pv.BusinessEntityID equals v.BusinessEntityID
                             where SqlMethods.Like(p.Name, productName)
                             select v.Name).FirstOrDefault();

            return query;
        }

        public List<Product> GetProductsWithNRecentReviews(int howManyReviews)
        {
            List<Product> query = (from pr in db.ProductReview
                    join p in db.Products on pr.ProductID equals p.ProductID
                    select p)
                    .Take(howManyReviews).ToList<Product>();

            return query;
        }
        public List<Product> GetNRecentlyReviewedProducts(int howManyProducts)
        {
            List<Product> query = (from p in db.Products
                    join pr in db.ProductReview on p.ProductID equals pr.ProductID
                    orderby pr.ReviewDate
                    select p)
                    .Take(howManyProducts).ToList<Product>();

            return query;
        }
        public List<Product> GetNProductsFromCategory(string categoryName, int n)
        {
            List<Product> query = (from p in db.Products
                    join ps in db.ProductSubcategory on p.ProductSubcategoryID equals ps.ProductSubcategoryID
                    join pc in db.ProductCategory on ps.ProductCategoryID equals pc.ProductCategoryID
                    where pc.Name == categoryName
                    select p)
                    .Take(n).ToList();

            return query;
        }

        public double GetTotalStandardCostByCategory(ProductCategory category)
        {
            double query =  (from p in db.Products
                    join ps in db.ProductSubcategory on p.ProductSubcategoryID equals ps.ProductSubcategoryID
                    join pc in db.ProductCategory on ps.ProductCategoryID equals pc.ProductCategoryID
                    where pc.Equals(category)
                    select p)
                    .Sum(p => Convert.ToDouble(p.ListPrice));

            return query;
        }


    }
}
