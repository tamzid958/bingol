using Bingol.Models;
using Bingol.Data;
using Bingol.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Specialized;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Bingol.Areas.Identity.Data;

namespace Bingol.Controllers
{
    public class ProductsController : Controller
    {
        private readonly BingolContext _db;
        private readonly UserManager<BingolUser> _userManager;
        //we can get from email notification
        private const string StoreId = "theby5f956bcb364af";
        private const string StorePassword = "theby5f956bcb364af@ssl";
        private const bool IsSandboxMode = true;
        private const string Currency = "BDT";

        public ProductsController(BingolContext db, UserManager<BingolUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public IQueryable<Product> SearchProduct(string searching, int category, int color, int size, string sorted, int price)
        {
            var products = _db.Products.AsQueryable();
            if (!string.IsNullOrEmpty(searching))
            {
                products = products.Where(o => o.ProductName.ToLower().Contains(searching.ToLower()));
            }
            if (price > 0)
            {
                products = products.Where(o => o.ProductPrice < price);
            }
            if (category > 0)
            {
                products = products.Where(o => o.ProductCategory.CategoryId == category);
            }
            if (color > 0)
            {
                products = products.Where(o => o.Productoptions.Any(a => a.OptionId == color));
            }
            if (size > 0)
            {
                products = products.Where(o => o.Productoptions.Any(a => a.OptionId == size));
            }

            if (string.IsNullOrEmpty(sorted)) return products;
            {
                products = sorted switch
                {
                    "asce" => products.OrderBy(o => o.ProductId),
                    "desc" => products.OrderByDescending(o => o.ProductId),
                    _ => products
                };
            }
            return products;
        }
        public async Task<IActionResult> Index(string searching, int category, int color, int size, string sorted, int price, int page=1)
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
            var productsIndex = _db.Products;
            var options = _db.Options;
            ViewBag.TotalProducts = productsIndex.Count();
            ViewBag.maxProductPrice = (int)Math.Ceiling(productsIndex.AsQueryable().Max(o => o.ProductPrice));
            ViewBag.minProductPrice = (int)Math.Ceiling(productsIndex.AsQueryable().Min(o => o.ProductPrice));
            var products = SearchProduct(searching, category, color, size, sorted, price);
            dynamic metamodel = new ExpandoObject();
            metamodel.Products = await PaginatedList<Product>.CreateAsync(products.Include(m => m.ProductCategory).OrderByDescending(m => m.ProductId), page, 12);
            metamodel.Categories = _db.Productcategories;
            metamodel.Color = options.Where(o => o.OptionsGroup.OptionGroupName.ToLower() == "color");
            metamodel.Size = options.Where(o => o.OptionsGroup.OptionGroupName.ToLower() == "size");
            return View(metamodel);
        }
        
        public async Task<IActionResult> ProductAsync(int? id)
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
            if (id == null)
            {
                return NotFound();
            }
            dynamic metamodel = new ExpandoObject();
            metamodel.Product = await _db.Products.Include(m => m.ProductCategory).FirstOrDefaultAsync(m => m.ProductId == id);
            if (metamodel.Product == null)
            {
                return NotFound();
            }
            metamodel.ProductSizeOptions = _db.Options.Where(o => o.OptionsGroupId == 2 && o.Productoptions.Any(m => m.ProductId == id));
            metamodel.ProductColorOptions = _db.Options.Where(o => o.OptionsGroupId == 1 && o.Productoptions.Any(m => m.ProductId == id));
            var category = _db.Productcategories.FirstOrDefault(o => o.Products.Any(m => m.ProductId == id));
            metamodel.SimilarProducts = _db.Products.Include(m => m.ProductCategory).Where(m => m.ProductCategory.CategoryId == category.CategoryId && m.ProductId != id).OrderByDescending(o => o.ProductId).Take(12);
            metamodel.VarientProducts = _db.Products.Include(m => m.ProductCategory).Where(m => m.ProductCategory.CategoryId != category.CategoryId && m.ProductId != id).OrderByDescending(o => o.ProductId).Take(12);
            return metamodel.Product == null ? NotFound() : (IActionResult) View(metamodel);
        }
        
        [Authorize]
        [Route("/{action=Index}")]
        public async Task<IActionResult> CartAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            ViewBag.UserFirstName = user.UserFirstName;
            ViewBag.UserLastName = user.UserLastName;
            ViewBag.UserName = user.NormalizedUserName;
            ViewBag.Email = user.Email;
            ViewBag.PhoneNumber = user.PhoneNumber;
            ViewBag.BillingAddress = user.UserAddress;
            ViewBag.ShippingAddress = user.UserAddress2;
            ViewBag.Country = user.UserCountry;
            ViewBag.City = user.UserCity;
            ViewBag.State = user.UserState;
            ViewBag.ZipCode = user.UserZip;
            return View();
        }

        [Authorize]
        public IActionResult Checkout()
        {
            const string productName = "Blue Jeans";
            const int price = 8500;
            var baseUrl = Request.Scheme + "://" + Request.Host;

            var postData = new NameValueCollection
            {
                { "total_amount", $"{price}" },
                { "tran_id", "Bingol" },
                { "success_url", baseUrl + "/Identity/Account/Manage/Orders" },
                { "fail_url", baseUrl + "/CheckoutFail" },
                { "cancel_url", baseUrl + "/CheckoutCancel" },
                { "version", "3.00" },
                { "cus_name", "ABC XY" },
                { "cus_email", "abc.xyz@mail.co" },
                { "cus_add1", "Address Line On" },
                { "cus_add2", "Address Line Tw" },
                { "cus_city", "City Nam" },
                { "cus_state", "State Nam" },
                { "cus_postcode", "Post Cod" },
                { "cus_country", "Country" },
                { "cus_phone", "0111111111" },
                { "cus_fax", "0171111111" },
                { "ship_name", "ABC XY" },
                { "ship_add1", "Address Line On" },
                { "ship_add2", "Address Line Tw" },
                { "ship_city", "City Nam" },
                { "ship_state", "State Nam" },
                { "ship_postcode", "Post Cod" },
                { "ship_country", "Country" },
                { "value_a", "ref00" },
                { "value_b", "ref00" },
                { "value_c", "ref00" },
                { "value_d", "ref00" },
                { "shipping_method", "NO" },
                { "num_of_item", "1" },
                { "product_name", $"{productName}" },
                { "product_profile", "general" },
                { "product_category", "Demo" }
            };


            var sslv = new SslCommerzGatewayProcessor(StoreId, StorePassword, IsSandboxMode);
            var response = sslv.InitiateTransaction(postData);

            return Redirect(response);
        }

        [Authorize]
        [Route("/{action=Index}")]
        public IActionResult CheckoutConfirmation()
        {
            if (!(!string.IsNullOrEmpty(Request.Form["status"]) && Request.Form["status"] == "VALID"))
            {
                ViewBag.SuccessInfo = "There some error while processing your payment. Please try again.";
                return RedirectToAction("CheckoutFail");
            }

            string trxId = Request.Form["tran_id"];
            // AMOUNT and Currency FROM DB FOR THIS TRANSACTION
            const string amount = "85000";
            var sslv = new SslCommerzGatewayProcessor(StoreId, StorePassword, IsSandboxMode);
            var response = sslv.OrderValidate(trxId, amount, Currency, Request);
            var successInfo = $"Validation Response: {response}";
            ViewBag.SuccessInfo = successInfo;

            return RedirectToAction("Cart");
        }

        [Authorize]
        [Route("/{action=Index}")]
        public IActionResult CheckoutFail()
        {
            ViewBag.FailInfo = "There some error while processing your payment. Please try again.";
            return View();
        }

        [Authorize]
        [Route("/{action=Index}")]
        public IActionResult CheckoutCancel()
        {
            ViewBag.CancelInfo = "Your payment has been cancel";
            return View();
        }
    }
}
