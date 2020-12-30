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
            DataBaseContext db = new DataBaseContext("Data Source=localhost;Initial Catalog=TWIERDZA\\ARTURROSERVER; Integrated Security=True");
            return (from p in db.Products
                    where SqlMethods.Like(p.Name, "%" + namePart + "%")
                    select p)
                    .ToList();
        }

        //TODO vendors

        public static List<Product> GetProductsWithNRecentReviews(int howManyReviews)
        {
            DataBaseContext db = new DataBaseContext("Data Source=localhost;Initial Catalog=TWIERDZA\\ARTURROSERVER; Integrated Security=True");

            return (from pr in db.ProductReviews
                    join p in db.Products on pr.ProductID equals p.ProductID
                    select p)
                    .Take(howManyReviews).ToList<Product>();
        }
        public static List<Product> GetNRecentlyReviewedProducts(int howManyProducts)
        {
            DataBaseContext db = new DataBaseContext("Data Source=localhost;Initial Catalog=TWIERDZA\\ARTURROSERVER; Integrated Security=True");

            return (from p in db.Products
                    join pr in db.ProductReviews on p.ProductID equals pr.ProductID
                    orderby pr.ReviewDate
                    select p)
                    .Take(howManyProducts).ToList<Product>();
        }
        public static List<Product> GetNProductsFromCategory(string categoryName, int n)
        {
            DataBaseContext db = new DataBaseContext("Data Source=localhost;Initial Catalog=TWIERDZA\\ARTURROSERVER; Integrated Security=True");

            return (from p in db.Products
                    join ps in db.ProductSubcategories on p.ProductSubcategoryID equals ps.ProductSubcategoryID
                    join pc in db.ProductCategories on ps.ProductCategoryID equals pc.ProductCategoryID
                    where pc.Name == categoryName
                    select p)
                    .Take(n).ToList();
        }

        public static double GetTotalStandardCostByCategory(ProductCategory category)
        {
            DataBaseContext db = new DataBaseContext("Data Source=localhost;Initial Catalog=TWIERDZA\\ARTURROSERVER; Integrated Security=True");

            return (from p in db.Products
                    join ps in db.ProductSubcategories on p.ProductSubcategoryID equals ps.ProductSubcategoryID
                    join pc in db.ProductCategories on ps.ProductCategoryID equals pc.ProductCategoryID
                    where pc.Equals(category)
                    select p)
                    .Sum(p => Convert.ToDouble(p.ListPrice));

        }
    }
}
