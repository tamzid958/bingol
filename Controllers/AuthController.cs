using Microsoft.AspNetCore.Mvc;

namespace Bingol.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Forgot_Password()
        {
            return View();
        }
    }
}
