using Bingol.Areas.Identity.Data;
using Bingol.Data;
using Bingol.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Bingol.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly BingolContext _db;
        private readonly UserManager<BingolUser> _userManager;
        private readonly SignInManager<BingolUser> _signInManager;
        private readonly ILogger<DashboardController> _logger;
        private readonly IEmailSender _emailSender;

        public DashboardController(BingolContext db,
            UserManager<BingolUser> userManager,
            SignInManager<BingolUser> signInManager,
            IEmailSender emailSender,
            ILogger<DashboardController> logger)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> OrdersAsync(int page = 1)
        {
            var orders = await PaginatedList<Order>.CreateAsync(_db.Orders.Include(m => m.OrderUser).Include(m => m.Orderdetails).OrderByDescending(o => o.OrderId), page, 30);
            return View(orders);
        }
        public async Task<IActionResult> ProductsAsync(int page = 1)
        {
            var products = await PaginatedList<Product>.CreateAsync(_db.Products.Include(m => m.ProductCategory).OrderByDescending(o => o.ProductId), page, 30);
            return View(products);
        }
        public async Task<IActionResult> CustomersAsync(int page = 1)
        {
            var customers = await PaginatedList<BingolUser>.CreateAsync(_db.BingolUsers, page, 30);
            return View(customers);
        }
        public async Task<IActionResult> ReviewsAsync(int page = 1)
        {
            var reviews = await PaginatedList<Review>.CreateAsync(_db.Reviews.Include(m => m.ReviewProduct).Include(m => m.ReviewUser), page, 30);
            return View(reviews);
        }


        private async Task LoadAsync(BingolUser user)
        {
            ViewBag.CurrentMail = await _userManager.GetEmailAsync(user);

        }

        public async Task<IActionResult> AccountAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return View();
        }
        public IActionResult AddProduct()
        {
            return View();
        }
        public IActionResult AddCategory()
        {
            return View();
        }
        public IActionResult AddOption()
        {
            return View();
        }
        public IActionResult UpdateProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return View();
        }
        public IActionResult UpdateCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return View();
        }
        public IActionResult UpdateOption(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return View();
        }
        public IActionResult DeleteProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _db.Products.First(x => x.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            _db.Products.Remove(product);
            _db.SaveChanges();
            return RedirectToAction("Products");
        }
        public IActionResult DeleteCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return RedirectToAction("Categories");
        }
        public IActionResult DeleteOption(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return RedirectToAction("Options");
        }
        public IActionResult DeleteCustomer(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return RedirectToAction("Customers");
        }
        public async Task<IActionResult> CategoriesAsync(int page = 1)
        {
            var categories = await PaginatedList<Productcategory>.CreateAsync(_db.Productcategories.OrderByDescending(m => m.CategoryId), page, 30);
            return View(categories);
        }
        public async Task<IActionResult> OptionsAsync(int page = 1)
        {
            var options = await PaginatedList<Option>.CreateAsync(_db.Options.Include(o => o.OptionsGroup).OrderByDescending(m => m.OptionId), page, 30);
            return View(options);
        }
    }
}
