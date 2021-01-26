using Bingol.Data;
using Bingol.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Bingol.Controllers
{
    public class ProductsController : Controller
    {
        private readonly BingolContext _db;

        public ProductsController(BingolContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> products = _db.Products;
            return View(products);
        }

        public IActionResult Product()
        {
            return View();
        }
    }
}
