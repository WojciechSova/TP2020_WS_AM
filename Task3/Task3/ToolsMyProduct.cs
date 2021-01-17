using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Linq;

namespace Task3
{
    public class ToolsMyProduct : IDisposable
    {
        private DataBaseDataContext db = null;
        public ToolsMyProduct()
        {
            db = new DataBaseDataContext();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public List<MyProduct> MyProductGetProductsByName(string namePart)
        {
            List<MyProduct> query = (from p in db.MyProduct
                                     where SqlMethods.Like(p.Name, "%" + namePart + "%")
                                     select p)
                                     .ToList();

            return query;
        }

        public List<MyProduct> MyProductGetProductsByVendorName(string vendorName)
        {
            List<MyProduct> query = (from p in db.MyProduct
                                     join pv in db.ProductVendors on p.ProductID equals pv.ProductID
                                     join v in db.Vendors on pv.BusinessEntityID equals v.BusinessEntityID
                                     where SqlMethods.Like(v.Name, vendorName)
                                     select p).ToList();

            return query;
        }

        public List<MyProduct> MyProductGetNRecentlyReviewedProducts(int howManyProducts)
        {
            List<MyProduct> query = (from p in db.MyProduct
                                     join pr in db.ProductReview on p.ProductID equals pr.ProductID
                                     orderby pr.ReviewDate
                                     select p)
                                     .Take(howManyProducts).ToList<MyProduct>();

            return query;
        }
    }
}
