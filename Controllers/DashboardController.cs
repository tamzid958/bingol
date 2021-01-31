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
        //normal view
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

        //list view
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


        public async Task<IActionResult> IndexAsync()
        {
            var checkColor = _db.Optiongroups.Where(o => o.OptionGroupName == "color");
            var checkSize = _db.Optiongroups.Where(o => o.OptionGroupName == "size");
            if (checkColor == null)
            {
                Optiongroup optionColor = new Optiongroup
                {
                    OptionGroupName = "color"
                };
                _db.Optiongroups.Add(optionColor);
            }
            if (checkSize == null)
            {
                Optiongroup optionColor = new Optiongroup
                {
                    OptionGroupName = "size"
                };
                _db.Optiongroups.Add(optionColor);
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            return View();
        }
        public async Task<IActionResult> AccountAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            return View(user);
        }
        public async Task<IActionResult> ChangePasswordAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
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
            var category = _db.Productcategories.First(o => o.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        public IActionResult UpdateOption(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return View();
        }

        //get section

        [HttpGet]
        public IActionResult DeleteCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = _db.Productcategories.First(o => o.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }
            _db.Productcategories.Remove(category);
            _db.SaveChanges();
            return RedirectToAction("Categories");
        }
        [HttpGet]
        public async Task<IActionResult> DeleteCustomerAsync(string id)
        {

            if (string.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }
            var customer = await _userManager.FindByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            if (await _userManager.IsInRoleAsync(customer, "Admin"))
            {
                return BadRequest("Admin Can't be deleted");
            }
            await _userManager.DeleteAsync(customer);
            return RedirectToAction("Customers");
        }
        public IActionResult DeleteOption(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return RedirectToAction("Options");
        }
        [HttpGet]
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


        //post section

        [HttpPost]
        public async Task<IActionResult> OnPostAccountDataChangeAsync(BingolUser model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Account");
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            user.UserFirstName = model.UserFirstName;
            user.UserLastName = model.UserLastName;
            user.PhoneNumber = model.PhoneNumber;
            user.Email = model.Email;
            user.UserName = model.Email;
            user.EmailConfirmed = true;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                ViewBag.StatusMessage = "Unexpected error when trying to set details.";
                return RedirectToAction("Account");
            }
            await _signInManager.RefreshSignInAsync(user);
            ViewBag.StatusMessage = "Your profile has been updated";
            return RedirectToAction("Account");
        }

        [HttpPost]
        public async Task<IActionResult> OnPostPasswordChangeAsync(ChangePassword model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ChangePassword");
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return RedirectToAction("ChangePassword");
            }

            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("User changed their password successfully.");
            ViewBag.StatusMessage = "Your password has been changed.";

            return RedirectToAction("ChangePassword");
        }
        [HttpPost]
        public IActionResult OnPostAddCategory(Productcategory model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("AddCategory");
            }
            _db.Productcategories.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Categories");
        }
        [HttpPost]
        public IActionResult OnPostUpdateCategoryDetails(Productcategory model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Categories");
            }
            var category = _db.Productcategories.First(o => o.CategoryId == model.CategoryId);
            if(category == null)
            {
                return RedirectToAction("Categories");
            }
            category.CategoryName = model.CategoryName;
            _db.Productcategories.Update(category);
            _db.SaveChanges();
            return RedirectToAction("Categories");
        } 
    }
}
