using Microsoft.AspNetCore.Mvc;

namespace Bingol.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
