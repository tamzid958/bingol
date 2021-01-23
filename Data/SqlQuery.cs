using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Bingol.Data
{
    public class SqlQuery
    {
        private readonly BingolContext context = new BingolContext();
        private readonly string allProductsQuery = "SELECT * FROM bingol.products";
        private readonly string allCategoriesQuery = "SELECT * FROM bingol.productcategories";
        public DataTable AllProducts()
        {
            return context.GetArray(allProductsQuery);
        }
        public DataTable AllCategories()
        {
            return context.GetArray(allCategoriesQuery);
        }
    }
}
