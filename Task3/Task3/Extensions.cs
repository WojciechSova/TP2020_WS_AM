using System;
using System.Collections.Generic;
using System.Linq;

namespace Task3
{
    public static class Extensions
    {
        public static List<Product> WithoutCategory(this List<Product> list)
        {
            IEnumerable<Product> withoutCategory = list.Where(p => p.ProductSubcategoryID == null);

            return withoutCategory.ToList();
        }

        public static List<Product> SplitIntoPages(this List<Product> list, int size, int page)
        {
            IEnumerable<Product> newPage = list.Skip(page * size).Take(size).ToList();

            return newPage.ToList();
        }

        public static String GetProductAndName(this List<Product> list)
        {
            DataBaseDataContext db = new DataBaseDataContext();
            String info = "";
            foreach (var product in list)
            {

                var query = new
                {

                    name =  (from p in list
                             join pv in product.ProductVendors on p.ProductID equals pv.ProductID
                             join v in db.Vendors on pv.BusinessEntityID equals v.BusinessEntityID
                             where product.ProductID == p.ProductID
                             select p.Name)
                             .FirstOrDefault(),

                    vendor = (from p in list
                              join pv in product.ProductVendors on p.ProductID equals pv.ProductID
                              join v in db.Vendors on pv.BusinessEntityID equals v.BusinessEntityID
                              where product.ProductID == p.ProductID
                              select v.Name)
                              .FirstOrDefault()

                };

                info += query.name.ToString() + " - " + query.vendor.ToString() + "\n";
            }
            return info;
        }

    }
}
