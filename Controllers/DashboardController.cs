using Microsoft.AspNetCore.Mvc;

namespace Bingol.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
