using Bingol.Areas.Identity.Data;
using Bingol.Data;
using Bingol.Helpers;
using Bingol.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
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
        public async Task<IActionResult> AddProductAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            ViewBag.ProductCategories = new SelectList(items: _db.Productcategories, "CategoryId", "CategoryName");
            return View();
        }
        public async Task<IActionResult> AddCategoryAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            return View();
        }
        public async Task<IActionResult> AddOptionAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            ViewBag.OptionsGroups = new SelectList(items: _db.Optiongroups, "OptionGroupId", "OptionGroupName");
            return View();
        }
        public async Task<IActionResult> AddVariationAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            ViewBag.Products = new SelectList(from s in _db.Products select new {ID = s.ProductId,ProductName = "#" + s.ProductId + " " + s.ProductName},"ID", "ProductName", null);
            ViewBag.Options = new SelectList(items: _db.Options, "OptionId", "OptionName");
            return View();
        }
        //list view
        public async Task<IActionResult> OrdersAsync(int page = 1)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var orders = await PaginatedList<Orderdetail>.CreateAsync(_db.Orderdetails.Include(o => o.DetailOrder).Include(o => o.DetailProduct).Include(o => o.DetailOrder.OrderUser).OrderByDescending(m => m.DetailId), page, 30);
            return View(orders);
        }
        public async Task<IActionResult> ProductsAsync(int page = 1)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var products = await PaginatedList<Product>.CreateAsync(_db.Products.Include(m => m.ProductCategory).OrderByDescending(o => o.ProductId), page, 30);
            return View(products);
        }
        public async Task<IActionResult> CustomersAsync(int page = 1)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var customers = await PaginatedList<BingolUser>.CreateAsync(_db.BingolUsers, page, 30);
            return View(customers);
        }
        public async Task<IActionResult> ReviewsAsync(int page = 1)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var reviews = await PaginatedList<Review>.CreateAsync(_db.Reviews.Include(m => m.ReviewProduct).Include(m => m.ReviewUser).OrderByDescending(m => m.ReviewId), page, 30);
            return View(reviews);
        }
        public async Task<IActionResult> CategoriesAsync(int page = 1)
        {
            var categories = await PaginatedList<Productcategory>.CreateAsync(_db.Productcategories.OrderByDescending(m => m.CategoryId), page, 30);
            return View(categories);
        }
        public async Task<IActionResult> OptionsAsync(int page = 1)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var options = await PaginatedList<Option>.CreateAsync(_db.Options.Include(o => o.OptionsGroup).OrderByDescending(m => m.OptionId), page, 30);
            return View(options);
        }
        public async Task<IActionResult> VariationsAsync(int page = 1)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var productOptions = await PaginatedList<Productoption>.CreateAsync(_db.Productoptions.Include(o => o.OptionGroup).Include(o => o.Product).Include(o => o.Option).OrderByDescending(m => m.OptionId), page, 30);
            return View(productOptions);
        }


        public async Task<IActionResult> IndexAsync()
        {
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

        public async Task<IActionResult> UpdateProductAsync(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            if (id == null)
            {
                return NotFound();
            }
            var product = _db.Products.First(o => o.ProductId == id);
            if(product == null)
            {
                return NotFound();
            }
            ViewBag.ProductCategories = new SelectList(items: _db.Productcategories, "CategoryId", "CategoryName");
            return View(product);
        }
        public async Task<IActionResult> UpdateCategoryAsync(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
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
        public async Task<IActionResult> UpdateOptionAsync(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            if (id == null)
            {
                return NotFound();
            }
            var option = _db.Options.First(o => o.OptionId == id);
            if(option == null)
            {
                return NotFound();
            }
            ViewBag.OptionsGroups = new SelectList(items: _db.Optiongroups, "OptionGroupId", "OptionGroupName");
            return View(option);
        }
        public async Task<IActionResult> UpdateOrderAsync(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            if (id == null)
            {
                return NotFound();
            }
            var order = _db.Orders.First(o => o.OrderId == id);
            return View(order);
        }

        //get section

        [HttpGet]
        public async Task<IActionResult> DeleteCategoryAsync(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
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
            await _db.SaveChangesAsync();
            return RedirectToAction("Categories");
        }
        [HttpGet]
        public async Task<IActionResult> DeleteCustomerAsync(string id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
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
        [HttpGet]
        public async Task<IActionResult> DeleteOptionAsync(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            if (id == null)
            {
                return NotFound();
            }
            var option = _db.Options.First(m => m.OptionId == id);
            if(option == null)
            {
                return NotFound();
            }
            _db.Options.Remove(option);
            await _db.SaveChangesAsync();
            return RedirectToAction("Options");
        }
        [HttpGet]
        public async Task<IActionResult> DeleteProductAsync(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
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
            await _db.SaveChangesAsync();
            return RedirectToAction("Products");
        }
        [HttpGet]
        public async Task<IActionResult> DeleteVariationAsync(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            if (id == null)
            {
                return NotFound();
            }
            var production = _db.Productoptions.First(x => x.ProductOptionId == id);
            if (production == null)
            {
                return NotFound();
            }
            _db.Productoptions.Remove(production);
            await _db.SaveChangesAsync();
            return RedirectToAction("Variations");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteReviewAsync(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            if (id == null)
            {
                return NotFound();
            }
            var review = _db.Reviews.First(x => x.ReviewId == id);
            if (review == null)
            {
                return NotFound();
            }
            _db.Reviews.Remove(review);
            await _db.SaveChangesAsync();
            return RedirectToAction("Reviews");
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
            _logger.LogInformation("User changed their password successfully");
            ViewBag.StatusMessage = "Your password has been changed.";

            return RedirectToAction("ChangePassword");
        }
        [HttpPost]
        public async Task<IActionResult> OnPostAddNewCategoryAsync(Productcategory model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("AddCategory");
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            await _db.Productcategories.AddAsync(model);
            await _db.SaveChangesAsync();
            return RedirectToAction("Categories");
        }
        [HttpPost]
        public async Task<IActionResult> OnPostUpdateCategoryDetailsAsync(Productcategory model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Categories");
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var category = _db.Productcategories.First(o => o.CategoryId == model.CategoryId);
            if(category == null)
            {
                return RedirectToAction("Categories");
            }
            category.CategoryName = model.CategoryName;
            _db.Productcategories.Update(category);
            await _db.SaveChangesAsync();
            return RedirectToAction("Categories");
        } 
        [HttpPost]
        public async Task<IActionResult> OnPostAddNewOptionAsync(Option model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("AddOption");
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            await _db.Options.AddAsync(model);
            await _db.SaveChangesAsync();
            return RedirectToAction("Options");
        }
        [HttpPost]
        public async Task<IActionResult> OnPostUpdateOptionDetailsAsync(Option model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Options");
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var option = _db.Options.First(o => o.OptionId == model.OptionId);
            if(option == null)
            {
                return NotFound();
            }
            option.OptionName = model.OptionName;
            option.OptionsGroupId = model.OptionsGroupId;
            _db.Options.Update(option);
            await _db.SaveChangesAsync();
            return RedirectToAction("Options");
        }

        [HttpPost]
        public async Task<IActionResult> OnPostAddNewProductAsync(Product model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("AddProduct");
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            model.ProductCartDesc = model.ProductShortDesc;
            model.ProductUpdateDate = DateTime.Now;
            model.ProductThumb = model.ProductImage;
            await _db.Products.AddAsync(model);
            await _db.SaveChangesAsync();
            return RedirectToAction("Products");
        }

        [HttpPost]
        public async Task<IActionResult> OnPostUpdateProductDetailsAsync(Product model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Products");
            }
            var product = _db.Products.First(o => o.ProductId == model.ProductId);
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            if (product == null)
            {
                return NotFound();
            }
            product.ProductSku = model.ProductSku;
            product.ProductName = model.ProductName;
            product.ProductPrice = model.ProductPrice;
            product.ProductWeight = model.ProductWeight;
            product.ProductCartDesc = model.ProductShortDesc;
            product.ProductLongDesc = model.ProductLongDesc;
            product.ProductThumb = model.ProductImage;
            product.ProductImage = model.ProductImage;
            product.ProductCategoryId = model.ProductCategoryId;
            product.ProductUpdateDate = DateTime.Now;
            product.ProductStock = model.ProductStock;
            product.ProductLive = model.ProductLive;
            product.ProductUnlimited = model.ProductUnlimited;
            product.ProductLocation = model.ProductLocation;
            _db.Products.Update(product);
            await _db.SaveChangesAsync();
            return RedirectToAction("Products");
        }

        [HttpPost]
        public async Task<IActionResult> OnPostAddNewVaritaionAsync(Productoption model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("AddVariation");
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var optionGroup = _db.Options.First(o => o.OptionId == model.OptionId);
            if(optionGroup == null)
            {
                return BadRequest("Something Wrong Happened");
            }

            if (optionGroup.OptionsGroupId != null) model.OptionGroupId = (int) optionGroup.OptionsGroupId;
            model.OptionPriceIncrement = 0;
            await _db.Productoptions.AddAsync(model);
            await _db.SaveChangesAsync();
            return RedirectToAction("Variations");
        }
        [HttpPost]
        public async Task<IActionResult> OnPostUpdateOrderDetailsAsync(Order model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Orders");
            }
            var order = _db.Orders.First(o => o.OrderId == model.OrderId);
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            if (order == null)
            {
                return NotFound();
            }
            order.OrderShipped = model.OrderShipped;
            _db.Orders.Update(order);
            await _db.SaveChangesAsync();
            return RedirectToAction("Orders");
        }
    }
}
