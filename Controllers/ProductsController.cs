using Bingol.Models;
using Bingol.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Bingol.Areas.Identity.Data;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using Bingol.Helpers;
using System.Collections.Generic;

namespace Bingol.Controllers
{
    public class ProductsController : Controller
    {
        private readonly BingolContext _db;
        private readonly UserManager<BingolUser> _userManager;
        private readonly SignInManager<BingolUser> _signInManager;
        //we can get from email notification
        private const string StoreId = "theby5f956bcb364af";
        private const string StorePassword = "theby5f956bcb364af@ssl";
        private const bool IsSandboxMode = true;
        private const string Currency = "BDT";

        public ProductsController(BingolContext db, UserManager<BingolUser> userManager, SignInManager<BingolUser> signInManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
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
            var productsIndex = _db.Products;
            var options = _db.Options;
            ViewBag.TotalProducts = productsIndex.Count();
            var products = SearchProduct(searching, category, color, size, sorted, price);
            dynamic metamodel = new ExpandoObject();
            metamodel.Products = await PaginatedList<Product>.CreateAsync(products.Include(m => m.ProductCategory).OrderByDescending(m => m.ProductId), page, 12);
            metamodel.Categories = _db.Productcategories;
            metamodel.Color = options.Where(o => o.OptionsGroup.OptionGroupName.ToLower() == "color");
            metamodel.Size = options.Where(o => o.OptionsGroup.OptionGroupName.ToLower() == "size");
            if (!productsIndex.Any()) return View(metamodel);
            ViewBag.maxProductPrice = (int) Math.Ceiling(productsIndex.AsQueryable().Max(o => o.ProductPrice));
            ViewBag.minProductPrice = (int) Math.Ceiling(productsIndex.AsQueryable().Min(o => o.ProductPrice));
            return View(metamodel);
        }

        [Route("/{action=Index}/{id?}")]
        public async Task<IActionResult> ProductAsync(int? id)
        {
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
            var productivity = _db.Products.First(o => o.ProductId == id);
            if (productivity.ProductStock <= 0)
            {
                productivity.ProductLive = 0;
                _db.Products.Update(productivity);
                await _db.SaveChangesAsync();
            }
            metamodel.ProductSizeOptions = _db.Options.Where(o => o.OptionsGroup.OptionGroupName.ToLower() == "size" && o.Productoptions.Any(m => m.ProductId == id));
            metamodel.ProductColorOptions = _db.Options.Where(o => o.OptionsGroup.OptionGroupName.ToLower() == "color" && o.Productoptions.Any(m => m.ProductId == id));
            var category = _db.Productcategories.First(o => o.Products.Any(m => m.ProductId == id));
            metamodel.SimilarProducts = _db.Products.Include(m => m.ProductCategory)
                .Where(m => m.ProductCategory.CategoryId == category.CategoryId && m.ProductId != id)
                .OrderByDescending(o => o.ProductId)
                .Take(12);
            metamodel.VarientProducts = _db.Products.Include(m => m.ProductCategory)
                .Where(m => m.ProductCategory.CategoryId != category.CategoryId && m.ProductId != id)
                .OrderByDescending(o => o.ProductId)
                .Take(12);
            var review = _db.Reviews.Where(o => o.ReviewProductId == id);
            var totalled = review.Count();
            var notochord = review.Count(o => o.ReviewRating == 1);
            var goodies = review.Count(o => o.ReviewRating == 2);
            var bettering = review.Count(o => o.ReviewRating == 3);
            var bestrew = review.Count(o => o.ReviewRating == 4);
            ViewBag.TotalReview = totalled;
            if (totalled == 0) return metamodel.Product == null ? NotFound() : (IActionResult) View(metamodel);
            ViewBag.NotGoodReview = 100 * notochord / totalled;
            ViewBag.GoodReview = 100 * goodies / totalled;
            ViewBag.BetterReview = 100 * bettering / totalled;
            ViewBag.BestReview = 100 * bestrew / totalled;
            return metamodel.Product == null ? NotFound() : (IActionResult) View(metamodel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AddtoCart(int id,
                                                   int quantity,
                                                   int color,
                                                   int size)
        {
            var user = await _userManager.GetUserAsync(User);
            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
            if(user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            if (isAdmin)
            {
                return BadRequest("Admin can't buy product");
            }
            if (id <= 0 || quantity <= 0)
            {
                TempData["message"] = "Something error happened";
                return RedirectToAction("Product", new { id });

            }
            var product = _db.Products.First(o => o.ProductId == id && o.ProductStock >= quantity);
            if(product == null)
            {
                TempData["message"] = "Out of Stock Recently";
                return RedirectToAction("Product", new { id });
            }

            var tempCart = new TempCart
            {
                ProductID = id,
                CustomerID = user.Id,
                Quantity = quantity,
                Color = color,
                Size = size
            };
            _db.TempCarts.Add(tempCart);
            _db.SaveChanges();
            TempData["success message"] = "Product added to Cart";
            return RedirectToAction("Product", new { id });
        }
        
        [Authorize]
        [Route("/{action=Index}")]
        public async Task<IActionResult> CartAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
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
            
            var tempCarts = _db.TempCarts.Where(o => o.CustomerID == user.Id).Include(o => o.Product);
            var total = Enumerable.Aggregate(tempCarts, 0.00, (current, obj) => current + obj.Product.ProductPrice * obj.Quantity);

            ViewBag.Counter = tempCarts.Count();
            ViewBag.UserId = user.Id;
            ViewBag.Total = Math.Round(total, 2);
            return View(tempCarts);
        }

        public IActionResult DeleteCartProduct(int? id)
        {
            if(id == null)
            {
                return BadRequest("Something error happened");
            }
            var tempCarts = _db.TempCarts.First(o =>o.TempCartID == id);
            if(tempCarts == null)
            {
                return BadRequest("Something error happened");
            }
            _db.TempCarts.Remove(tempCarts);
            _db.SaveChanges();
            return RedirectToAction("Cart");
        }

        private static string CreateMd5(string input)
        {
            // Use input string to calculate MD5 hash
            using var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            var sb = new StringBuilder();
            foreach (var t in hashBytes)
            {
                sb.Append(t.ToString("X2"));
            }
            return sb.ToString();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Checkout(string total,
                                      string userId,
                                      string firstname,
                                      string email,
                                      string phone,
                                      string city,
                                      string state,
                                      string zip,
                                      string country,
                                      string address1,
                                      string address2)
        {
            if (string.IsNullOrEmpty(total))
            {
                TempData["danger msg"] = $"'{nameof(total)}' cannot be null or empty";
                return RedirectToAction("Cart");
            }

            if (string.IsNullOrEmpty(userId))
            {
                TempData["danger msg"] = $"'{nameof(userId)}' cannot be null or empty";
                return RedirectToAction("Cart");
            }

            if (string.IsNullOrEmpty(firstname))
            {
                TempData["danger msg"] = $"'{nameof(firstname)}' cannot be null or empty";
                return RedirectToAction("Cart");
            }

            if (string.IsNullOrEmpty(email))
            {
                TempData["danger msg"] = $"'{nameof(email)}' cannot be null or empty";
                return RedirectToAction("Cart");
            }

            if (string.IsNullOrEmpty(phone))
            {
                TempData["danger msg"] = $"'{nameof(phone)}' cannot be null or empty";
                return RedirectToAction("Cart");
            }

            if (string.IsNullOrEmpty(city))
            {
                TempData["danger msg"] = $"'{nameof(total)}' cannot be null or empty";
                return RedirectToAction("Cart");
            }

            if (string.IsNullOrEmpty(state))
            {
                TempData["danger msg"] = $"'{nameof(state)}' cannot be null or empty";
                return RedirectToAction("Cart");
            }

            if (string.IsNullOrEmpty(zip))
            {
                TempData["danger msg"] = $"'{nameof(zip)}' cannot be null or empty";
                return RedirectToAction("Cart");
            }
            if (string.IsNullOrEmpty(country))
            {
                TempData["danger msg"] = $"'{nameof(country)}' cannot be null or empty";
                return RedirectToAction("Cart");
            }

            if (string.IsNullOrEmpty(address1))
            {
                TempData["danger msg"] = $"'{nameof(address1)}' cannot be null or empty";
                return RedirectToAction("Cart");
            }

            if (string.IsNullOrEmpty(address2))
            {
                TempData["danger msg"] = $"'{nameof(address2)}' cannot be null or empty";
                return RedirectToAction("Cart");
            }

            var tempCart = _db.TempCarts.Where(o => o.CustomerID == userId).Include(o => o.Product);
            var transId = CreateMd5(DateTime.Now.ToString(CultureInfo.InvariantCulture) + userId);
            var order = new Order
            {
                OrderUserId = userId,
                OrderAmount = tempCart.Count(),
                OrderShipName = "Bingol",
                OrderShipAddress = address1,
                OrderShipAddress2 = address2,
                OrderCity = city,
                OrderState = state,
                OrderZip = zip,
                OrderCountry = country,
                OrderPhone = phone,
                OrderFax = "deprecated",
                OrderShipping = 0,
                OrderTax = 0,
                OrderEmail = email,
                OrderDate = DateTime.Now,
                OrderShipped = 0,
                OrderTrackingNumber = transId,
            };
            _db.Orders.Add(order);
            _db.SaveChanges();

            var orderOrderDetail = _db.Orders.First(o => o.OrderTrackingNumber == transId);
            IList<Orderdetail> orderList = new List<Orderdetail>();
            
            foreach (var obj in tempCart)
            {
                var size = _db.Options.First(o => o.OptionId == obj.Size);
                var color = _db.Options.First(o => o.OptionId == obj.Color);
                var ordering = new Orderdetail
                {
                    DetailOrderId = orderOrderDetail.OrderId,
                    DetailProductId = obj.ProductID,
                    DetailName = size.OptionName + "," + color.OptionName,
                    DetailPrice = (float)Math.Round((obj.Product.ProductPrice * obj.Quantity), 2),
                    DetailSku = obj.Product.ProductSku,
                    DetailQuantity = obj.Quantity
                };
                orderList.Add(ordering);
                var product = _db.Products.First(o => o.ProductId == obj.ProductID);
                product.ProductStock -= obj.Quantity;
                _db.Update(product);
            }

            _db.Orderdetails.AddRange(orderList);

            _db.TempCarts.RemoveRange(tempCart.ToList());
            _db.SaveChanges();

            TempData["success msg"] = "Purchased Successfully";
            return RedirectToAction("Cart");
        }
        

        [Authorize]
        public async Task<IActionResult> AddtoWishlist(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            if (isAdmin)
            {
                return BadRequest("Admin can't wishlist product");
            }
            if (id <= 0)
            {
                TempData["message"] = "Something error happened";
                return RedirectToAction("Product", new { id });

            }
            var wishlist = new Wishlist
            {
                WishlistProductId = id,
                WishlistUserId = user.Id,
                WishlistCondition = 1
            };
            _db.Wishlists.Add(wishlist);
            _db.SaveChanges();
            TempData["success message"] = "Added to Wishlist";
            return RedirectToAction("Product", new { id });
        }
    }
}
