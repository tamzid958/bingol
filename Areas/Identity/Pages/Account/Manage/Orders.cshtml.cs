using System.Threading.Tasks;
using Bingol.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Bingol.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Bingol.Models;

namespace Bingol.Areas.Identity.Pages.Account.Manage
{
    [Authorize(Roles = "Customer")]
    public partial class OrdersModel : PageModel
    {
        private readonly BingolContext _db;
        private readonly UserManager<BingolUser> _userManager;
        private readonly SignInManager<BingolUser> _signInManager;
        public OrdersModel(BingolContext db,
            UserManager<BingolUser> userManager,
            SignInManager<BingolUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var userId = _userManager.GetUserId(User);
            ViewData["OrderList"] = _db.Orderdetails.Where(o => o.DetailOrder.OrderUserId == userId).Include(o => o.DetailOrder).Include(o => o.DetailProduct).OrderByDescending(m => m.DetailId).ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostLeavingReviewAsync(int productid, int reviews, int orderid)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var userId = _userManager.GetUserId(User);
            var review = new Review
            {
                ReviewProductId = productid,
                ReviewUserId = userId,
                ReviewOrderId = orderid,
                ReviewRating = reviews
            };
            var order = _db.Orderdetails.First(o => o.DetailId == orderid);
            order.Reviewed = true;
            
            _db.Orderdetails.Update(order);
            await _db.SaveChangesAsync();

            await _db.Reviews.AddAsync(review);
            await _db.SaveChangesAsync();
            return RedirectToPage("Orders");
        }

    }
}