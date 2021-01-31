using Bingol.Data;
using Bingol.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;

namespace Bingol.Controllers
{
    public class HomeController : Controller
    {
        private readonly BingolContext _db;
        private readonly ILogger<HomeController> _logger;

        public HomeController(BingolContext db, ILogger<HomeController> logger)
        {
            _db = db;
            _logger = logger;
        }

        public IActionResult Index()
        {
            dynamic metamodel = new ExpandoObject();
            metamodel.newOnthisSite = _db.Products.OrderByDescending(o => o.ProductId).Take(6);
            metamodel.dealsOftheWeek = _db.Products.OrderBy(o => o.ProductPrice).Take(6);
            return View(metamodel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
