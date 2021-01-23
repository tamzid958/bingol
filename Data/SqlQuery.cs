using System.Data;

namespace Bingol.Data
{
    public class SqlQuery
    {
        private const string AllProductsQuery = "SELECT * FROM bingol.products";
        private const string AllCategoriesQuery = "SELECT * FROM bingol.productcategories";

        public static DataTable AllProducts()
        {
            return BingolContext.GetArray(AllProductsQuery);
        }
        public static DataTable AllCategories()
        {
            return BingolContext.GetArray(AllCategoriesQuery);
        }
    }
}
