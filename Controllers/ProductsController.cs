using Bingol.Data;
using Bingol.Models;
using Bingol.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Bingol.Controllers
{
    public class ProductsController : Controller
    {
        private readonly BingolContext _db;
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
    }
}
