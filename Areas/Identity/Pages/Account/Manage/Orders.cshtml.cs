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
using System.Collections.Generic;

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
            var userID = _userManager.GetUserId(User);
            ViewData["OrderList"] = _db.Orders.Where(o => o.OrderUser.Id == userID).Include(m => m.Orderdetails).ToList();
            return Page();
        }

    }
}