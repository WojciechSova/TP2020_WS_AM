using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    class Tools
    {
        public static List<Product> GetProductsByName(string namePart)
        {
            DataBaseDataContext db = new DataBaseDataContext();
            return (from p in db.Products
                    where SqlMethods.Like(p.Name, "%" + namePart + "%")
                    select p)
                    .ToList();
        }

        public static List<Product> GetProductsByVendorName(string vendorName)
        {
            DataBaseDataContext db = new DataBaseDataContext();

            List<Product> query = (from p in db.Products
                                   join pv in db.ProductVendors on p.ProductID equals pv.ProductID
                                   join v in db.Vendors on pv.BusinessEntityID equals v.BusinessEntityID
                                   where SqlMethods.Like(v.Name, vendorName)
                                   select p).ToList();

            return query;
        }

        public static List<string> GetProductNamesByVendorName(string vendorName)
        {
            DataBaseDataContext db = new DataBaseDataContext();

            List<string> query = (from p in db.Products
                                  join pv in db.ProductVendors on p.ProductID equals pv.ProductID
                                  join v in db.Vendors on pv.BusinessEntityID equals v.BusinessEntityID
                                  where SqlMethods.Like(v.Name, vendorName)
                                  select p.Name).ToList();

            return query;
        }
        public static string GetProductVendorByProductName(string productName)
        {
            DataBaseDataContext db = new DataBaseDataContext();

            string query= (from p in db.Products
                             join pv in db.ProductVendors on p.ProductID equals pv.ProductID
                             join v in db.Vendors on pv.BusinessEntityID equals v.BusinessEntityID
                             where SqlMethods.Like(p.Name, productName)
                             select v.Name).FirstOrDefault();

            return query;
        }

        public static List<Product> GetProductsWithNRecentReviews(int howManyReviews)
        {
            DataBaseDataContext db = new DataBaseDataContext();

            List<Product> query = (from pr in db.ProductReview
                    join p in db.Products on pr.ProductID equals p.ProductID
                    select p)
                    .Take(howManyReviews).ToList<Product>();

            return query;
        }
        public static List<Product> GetNRecentlyReviewedProducts(int howManyProducts)
        {
            DataBaseDataContext db = new DataBaseDataContext();

            List<Product> query = (from p in db.Products
                    join pr in db.ProductReview on p.ProductID equals pr.ProductID
                    orderby pr.ReviewDate
                    select p)
                    .Take(howManyProducts).ToList<Product>();

            return query;
        }
        public static List<Product> GetNProductsFromCategory(string categoryName, int n)
        {
            DataBaseDataContext db = new DataBaseDataContext();

            List<Product> query = (from p in db.Products
                    join ps in db.ProductCategory on p.ProductSubcategoryID equals ps.ProductCategoryID
                    join pc in db.ProductCategory on ps.ProductCategoryID equals pc.ProductCategoryID
                    where pc.Name == categoryName
                    select p)
                    .Take(n).ToList();

            return query;
        }

        public static double GetTotalStandardCostByCategory(ProductCategory category)
        {
            DataBaseDataContext db = new DataBaseDataContext();

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
