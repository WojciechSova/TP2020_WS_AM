using System;
using System.Collections.Generic;
using System.Linq;

namespace Task3
{
    public static class Extensions
    {
        public static List<Product> WithoutCategory(this List<Product> lista)
        {
            List<Product> withoutCategory = new List<Product>();

            foreach (Product P in lista)
            {
                if (P.ProductSubcategoryID == null) withoutCategory.Add(P);
            }

            return withoutCategory;
        }

        public static List<Product> SplitIntoPages(this List<Product> list, int size, int page)
        {
            List<Product> newPage = new List<Product>();

            for (int i = 0; i < list.Count(); i++)
            {
                if (i >= size * page && i < (size * page) + size) 
                { 
                    newPage.Add(list[i]); 
                }
            }

            return newPage;
        }

        public static String GetProductAndName(this List<Product> list)
        {
            DataBaseDataContext db = new DataBaseDataContext();
            String info = "";
            foreach (var product in list)
            {

                var query = new
                {

                    name =  (from p in db.Products
                             join pv in db.ProductVendors on p.ProductID equals pv.ProductID
                             join v in db.Vendors on pv.BusinessEntityID equals v.BusinessEntityID
                             where product.ProductID == p.ProductID
                             select p.Name)
                             .FirstOrDefault(),

                    vendor = (from p in db.Products
                              join pv in db.ProductVendors on p.ProductID equals pv.ProductID
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
