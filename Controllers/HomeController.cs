using Bingol.Data;
using Bingol.Models;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment _env;
        public HomeController(BingolContext db, ILogger<HomeController> logger, IWebHostEnvironment env)
        {
            _db = db;
            _logger = logger;
            _env = env;
        }

        public IActionResult Index()
        {
            var sizeCheck = _db.Optiongroups.First(o => o.OptionGroupName == "size");
            var colorCheck = _db.Optiongroups.First(o => o.OptionGroupName == "color");
            if(sizeCheck == null)
            {
                var size = new Optiongroup
                {
                    OptionGroupId = 2,
                    OptionGroupName = "size"
                };
                _db.Optiongroups.Add(size);
                _db.SaveChanges();
            }
            if (colorCheck == null)
            {
                var color = new Optiongroup
                {
                    OptionGroupId = 1,
                    OptionGroupName = "color"
                };
                _db.Optiongroups.Add(color);
                _db.SaveChanges();
            }
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
