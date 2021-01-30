using System.Threading.Tasks;
using Bingol.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace Bingol.Areas.Identity.Pages.Account.Manage
{
    [Authorize(Roles = "Customer")]
    public partial class WishlistModel : PageModel
    {
        private readonly UserManager<BingolUser> _userManager;
        private readonly SignInManager<BingolUser> _signInManager;

        public WishlistModel(
            UserManager<BingolUser> userManager,
            SignInManager<BingolUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            return Page();
        }

    }
}