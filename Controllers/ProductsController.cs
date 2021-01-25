using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Bingol.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Product()
        {
            return View();
        }
    }
}
