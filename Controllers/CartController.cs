using Microsoft.AspNetCore.Mvc;

namespace Bingol.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
