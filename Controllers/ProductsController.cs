using Bingol.Data;
using Bingol.Models;
using Bingol.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Specialized;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Bingol.Controllers
{
    public class ProductsController : Controller
    {
        private readonly BingolContext _db;
        //we can get from email notificaton
        private readonly string storeId = "theby5f956bcb364af";
        private readonly string storePassword = "theby5f956bcb364af@ssl";
        private readonly bool isSandboxMode = true;
        private readonly string currency = "BDT";
        public ProductsController(BingolContext db)
        {
            _db = db;
        }

        public IQueryable<Product> SearchProduct(string searching, int category, int color, int size, string sorted)
        {
            var products = _db.Products.AsQueryable();
            if (!string.IsNullOrEmpty(searching))
            {
                products = products.Where(o => o.ProductName.ToLower().Contains(searching.ToLower()));
            }
            if (category > 0)
            {
                products = products.Where(o => o.ProductCategory.CategoryId == category);
            }
            if (color > 0)
            {

            }
            if (size > 0)
            {

            }
            if (!string.IsNullOrEmpty(sorted))
            {
                switch (sorted)
                { 
                    case "asce":
                        products = products.OrderBy(o => o.ProductId);
                        break;
                    case "desc":
                        products = products.OrderByDescending(o => o.ProductId);
                        break;
                }
            }
            return products;
        }
        public async Task<IActionResult> Index(string searching, int category, int color, int size, string sorted, int page=1)
        {
           
            var products = SearchProduct(searching, category, color, size, sorted);
            dynamic mymodel = new ExpandoObject();
            mymodel.Products = await PaginatedList<Product>.CreateAsync(products.Include(m => m.ProductCategory), page, 12); ;
            mymodel.Categories = _db.Productcategories;
            mymodel.Color = _db.Options.Where(o => o.OptionsGroup.OptionGroupId == 1);
            mymodel.Size = _db.Options.Where(o => o.OptionsGroup.OptionGroupId == 2);
            return View(mymodel);
        }
        
        public async Task<IActionResult> ProductAsync(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var product = await _db.Products.FirstOrDefaultAsync(m => m.ProductId == id);
            if(product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        public IActionResult Cart()
        {
            return View();
        }

        public IActionResult Checkout()
        {
            var productName = "Blue Jeans";
            var price = 8500;
            var baseUrl = Request.Scheme + "://" + Request.Host;

            NameValueCollection PostData = new NameValueCollection
            {
                { "total_amount", $"{price}" },
                { "tran_id", "TESTASPNET1234" },
                { "success_url", baseUrl + "/Identity/Account/Manage/Orders" },
                { "fail_url", baseUrl + "/Products/CheckoutFail" },
                { "cancel_url", baseUrl + "/Products/CheckoutCancel" },
                { "version", "3.00" },
                { "cus_name", "ABC XY" },
                { "cus_email", "abc.xyz@mail.co" },
                { "cus_add1", "Address Line On" },
                { "cus_add2", "Address Line Tw" },
                { "cus_city", "City Nam" },
                { "cus_state", "State Nam" },
                { "cus_postcode", "Post Cod" },
                { "cus_country", "Countr" },
                { "cus_phone", "0111111111" },
                { "cus_fax", "0171111111" },
                { "ship_name", "ABC XY" },
                { "ship_add1", "Address Line On" },
                { "ship_add2", "Address Line Tw" },
                { "ship_city", "City Nam" },
                { "ship_state", "State Nam" },
                { "ship_postcode", "Post Cod" },
                { "ship_country", "Countr" },
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


            SSLCommerzGatewayProcessor sslcz = new SSLCommerzGatewayProcessor(storeId, storePassword, isSandboxMode);
            string response = sslcz.InitiateTransaction(PostData);

            return Redirect(response);
        }

        public IActionResult CheckoutConfirmation()
        {
            if (!(!string.IsNullOrEmpty(Request.Form["status"]) && Request.Form["status"] == "VALID"))
            {
                ViewBag.SuccessInfo = "There some error while processing your payment. Please try again.";
                return View();
            }

            string TrxID = Request.Form["tran_id"];
            // AMOUNT and Currency FROM DB FOR THIS TRANSACTION
            string amount = "85000";
            SSLCommerzGatewayProcessor sslcz = new SSLCommerzGatewayProcessor(storeId, storePassword, isSandboxMode);
            var resonse = sslcz.OrderValidate(TrxID, amount, currency, Request);
            var successInfo = $"Validation Response: {resonse}";
            ViewBag.SuccessInfo = successInfo;

            return View();
        }
        public IActionResult CheckoutFail()
        {
            ViewBag.FailInfo = "There some error while processing your payment. Please try again.";
            return View();
        }
        public IActionResult CheckoutCancel()
        {
            ViewBag.CancelInfo = "Your payment has been cancel";
            return View();
        }
    }
}
