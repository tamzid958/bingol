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
            var orders = await PaginatedList<Order>.CreateAsync(_db.Orders.Include(m => m.OrderUser).Include(m => m.Orderdetails).OrderByDescending(o => o.OrderId), page, 12);
            return View(orders);
        }
        public async Task<IActionResult> ProductsAsync(int page = 1)
        {
            var products = await PaginatedList<Product>.CreateAsync(_db.Products.Include(m => m.ProductCategory).OrderByDescending(o => o.ProductId), page, 12);
            return View(products);
        }
        public async Task<IActionResult> CustomersAsync(int page = 1)
        {
            var customers = await PaginatedList<BingolUser>.CreateAsync(_db.BingolUsers, page, 12);
            return View(customers);
        }
        public async Task<IActionResult> ReviewsAsync(int page = 1)
        {
            var reviews = await PaginatedList<Review>.CreateAsync(_db.Reviews.Include(m => m.ReviewProduct).Include(m => m.ReviewUser), page, 12);
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
    }
}
