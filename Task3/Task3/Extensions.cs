using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                if (i >= size * page && i < (size * page) + size) newPage.Add(list[i]);
            }

            return newPage;
        }

    }
}
