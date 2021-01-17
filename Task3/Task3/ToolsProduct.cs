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
            IEnumerable<Product> query = (from p in db.Products
                                          where SqlMethods.Like(p.Name, "%" + namePart + "%")
                                          select p);

            return query.ToList(); ;
        }

        public List<Product> GetProductsByVendorName(string vendorName)
        {
            IEnumerable<Product> query = (from p in db.Products
                                          join pv in db.ProductVendors on p.ProductID equals pv.ProductID
                                          join v in db.Vendors on pv.BusinessEntityID equals v.BusinessEntityID
                                          where SqlMethods.Like(v.Name, vendorName)
                                          select p);

            return query.ToList(); ;
        }

        public List<string> GetProductNamesByVendorName(string vendorName)
        {
            IEnumerable<string> query = (from p in db.Products
                                         join pv in db.ProductVendors on p.ProductID equals pv.ProductID
                                         join v in db.Vendors on pv.BusinessEntityID equals v.BusinessEntityID
                                         where SqlMethods.Like(v.Name, vendorName)
                                         select p.Name);

            return query.ToList(); ;
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
            IEnumerable<Product> query = (from pr in db.ProductReview
                                          join p in db.Products on pr.ProductID equals p.ProductID
                                          select p).Take(howManyReviews);

            return query.ToList(); ;
        }
        public List<Product> GetNRecentlyReviewedProducts(int howManyProducts)
        {
            IEnumerable<Product> query = (from p in db.Products
                                          join pr in db.ProductReview on p.ProductID equals pr.ProductID
                                          orderby pr.ReviewDate
                                          select p).Take(howManyProducts);

            return query.ToList();
        }
        public List<Product> GetNProductsFromCategory(string categoryName, int n)
        {
            IEnumerable<Product> query = (from p in db.Products
                                          join ps in db.ProductSubcategory on p.ProductSubcategoryID equals ps.ProductSubcategoryID
                                          join pc in db.ProductCategory on ps.ProductCategoryID equals pc.ProductCategoryID
                                          where pc.Name == categoryName
                                          select p).Take(n);

            return query.ToList(); ;
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
