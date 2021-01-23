using Bingol.Data;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Bingol.Controllers
{
    public class ProductsController : Controller
    {
        private DataTable allProducts;
        private SqlQuery Sql { get; } = new SqlQuery();

        [HttpGet]
        public IActionResult Index()
        {
            allProducts = SqlQuery.AllProducts();
            return View(allProducts);
        }

        public IActionResult Product()
        {
            return View();
        }
    }
}
