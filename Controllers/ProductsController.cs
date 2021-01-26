using Bingol.Data;
using Bingol.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bingol.Controllers
{
    public class ProductsController : Controller
    {
        private readonly BingolContext _db;

        public ProductsController(BingolContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index(int page=1)
        {
            return View(await PaginatedList<Product>.CreateAsync(_db.Products, page, 12));
        }

        public IActionResult Product()
        {
            return View();
        }
    }
}
