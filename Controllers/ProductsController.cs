using Bingol.Data;
using Bingol.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Bingol.Controllers
{
    public class ProductsController : Controller
    {
        private DataTable allProducts;
        private SqlQuery Sql { get; } = new SqlQuery();

        [HttpGet]
        public IActionResult Index()
        {
            allProducts = Sql.AllProducts();
            return View(allProducts);
        }

        public IActionResult Product()
        {
            return View();
        }
    }
}
