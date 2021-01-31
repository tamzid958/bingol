using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Bingol.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System;
using System.Net;

namespace Bingol.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<BingolUser> _signInManager;
        private readonly UserManager<BingolUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private const string IchanZip = "https://ipv4.icanhazip.com/";
        public RegisterModel(
            UserManager<BingolUser> userManager,
            SignInManager<BingolUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "FirstName")]
            public string FirstName { get;  set; }
            
            [Required]
            [Display(Name = "LastName")]
            public string LastName { get; set; }
            
            [Required]
            [Display(Name = "Phone Number")]
            public string PhoneNumber { get;  set; }
            
            [Required]
            [Display(Name = "City")]
            public string City { get;  set; }
            
            [Required]
            [Display(Name = "State")]
            public string State { get;  set; }
            
            [Required]
            [Display(Name = "Zip")]
            public string ZipCode { get;  set; }
            
            [Required]
            [Display(Name = "Country")]
            public string Country { get;  set; }
            
            [Required]
            [Display(Name = "Billing Address")]
            public string BillingAddress { get;  set; }
            
            [Required]
            [Display(Name = "Shipping Address")]
            public string ShippingAddress { get;  set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new BingolUser { UserFirstName= Input.FirstName, UserLastName =Input.LastName,
                UserName = Input.Email, Email = Input.Email, PhoneNumber = Input.PhoneNumber, UserCity = Input.City,
                UserState= Input.State, UserZip = Input.ZipCode, UserCountry= Input.Country, UserAddress= Input.BillingAddress,
                UserAddress2 = Input.ShippingAddress, UserRegistrationDate = DateTime.Now,
                    UserIp = new WebClient().DownloadString(IchanZip).TrimEnd(),
            };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    await _userManager.AddToRoleAsync(user, "Customer");
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: (area: "Identity", userId: user.Id, code, returnUrl),
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", (email: Input.Email, returnUrl));
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
