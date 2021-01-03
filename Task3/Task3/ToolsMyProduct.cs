using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Linq;

namespace Task3
{
    public class ToolsMyProduct
    {
        public static List<MyProduct> MyProductGetProductsByName(string namePart)
        {
            DataBaseDataContext db = new DataBaseDataContext();
            List<MyProduct> query = (from p in db.MyProduct
                                     where SqlMethods.Like(p.Name, "%" + namePart + "%")
                                     select p)
                                     .ToList();

            return query;
        }

        public static List<MyProduct> MyProductGetProductsByVendorName(string vendorName)
        {
            DataBaseDataContext db = new DataBaseDataContext();

            List<MyProduct> query = (from p in db.MyProduct
                                     join pv in db.ProductVendors on p.ProductID equals pv.ProductID
                                     join v in db.Vendors on pv.BusinessEntityID equals v.BusinessEntityID
                                     where SqlMethods.Like(v.Name, vendorName)
                                     select p).ToList();

            return query;
        }

        public static List<MyProduct> MyProductGetNRecentlyReviewedProducts(int howManyProducts)
        {
            DataBaseDataContext db = new DataBaseDataContext();

            List<MyProduct> query = (from p in db.MyProduct
                                     join pr in db.ProductReview on p.ProductID equals pr.ProductID
                                     orderby pr.ReviewDate
                                     select p)
                                     .Take(howManyProducts).ToList<MyProduct>();

            return query;
        }
    }
}
