using System.Threading.Tasks;
using Bingol.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Bingol.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Bingol.Areas.Identity.Pages.Account.Manage
{
    [Authorize(Roles = "Customer")]
    public partial class WishlistModel : PageModel
    {
        private readonly BingolContext _db;
        private readonly UserManager<BingolUser> _userManager;
        private readonly SignInManager<BingolUser> _signInManager;

        public WishlistModel(BingolContext db,
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
            ViewData["Wishlist"] = _db.Wishlists.Where(o => o.WishlistUser.Id == userId).Include(m => m.WishlistProduct).OrderByDescending(m => m.WishlistId).ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var wishlist = await _db.Wishlists.FindAsync(id);
            if (wishlist == null)
            {
                return NotFound();

            }
            _db.Wishlists.Remove(wishlist);
            await _db.SaveChangesAsync();

            return RedirectToPage("Wishlist");
        }
    }
}