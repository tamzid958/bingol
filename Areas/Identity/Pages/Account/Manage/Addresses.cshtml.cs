using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Bingol.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace Bingol.Areas.Identity.Pages.Account.Manage
{
    [Authorize(Roles = "Customer")]
    public partial class AddressesModel : PageModel
    {
        private readonly UserManager<BingolUser> _userManager;
        private readonly SignInManager<BingolUser> _signInManager;
        private readonly ILogger<AddressesModel> _logger;
        public AddressesModel(
            UserManager<BingolUser> userManager,
            SignInManager<BingolUser> signInManager,
            ILogger<AddressesModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }
        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "City")]
            public string City { get; set; }

            [Required]
            [Display(Name = "State")]
            public string State { get; set; }

            [Required]
            [Display(Name = "Zip")]
            public string ZipCode { get; set; }

            [Required]
            [Display(Name = "Country")]
            public string Country { get; set; }

            [Required]
            [Display(Name = "Billing Address")]
            public string BillingAddress { get; set; }

            [Required]
            [Display(Name = "Shipping Address")]
            public string ShippingAddress { get; set; }
        }


        private async Task LoadAsync(BingolUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            Username = userName;
            var city = user.UserCity;
            var state = user.UserState;
            var zip = user.UserZip;
            var country = user.UserCountry;
            var billingdAddress = user.UserAddress;
            var shippingAddress = user.UserAddress2;
            Input = new InputModel
            {
                City = city,
                State = state,
                ZipCode = zip,
                Country = country,
                BillingAddress = billingdAddress,
                ShippingAddress = shippingAddress
            };
        }
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {

                return Page();
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            user.UserCity = Input.City;
            user.UserState = Input.State;
            user.UserZip = Input.ZipCode;
            user.UserCountry = Input.Country;
            user.UserAddress = Input.BillingAddress;
            user.UserAddress2 = Input.ShippingAddress;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                StatusMessage = "Unexpected error when trying to set details.";
                return RedirectToPage();
            }
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();

        }
    }
}