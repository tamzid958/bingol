﻿using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Bingol.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bingol.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<BingolUser> _userManager;
        private readonly SignInManager<BingolUser> _signInManager;

        public IndexModel(
            UserManager<BingolUser> userManager,
            SignInManager<BingolUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Display(Name = "Username")]
        public string UserName { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }
        }
        private async Task LoadAsync(BingolUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            UserName = userName;
            var firstName = user.UserFirstName;
            var lastName = user.UserLastName;
            var phoneNumber = user.PhoneNumber;
            var email = user.Email;
            Input = new InputModel
            {
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                Email = email
            };
        }
        
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            switch (user)
            {
                case null:
                    return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                default:
                    if (await _userManager.IsInRoleAsync(user, "Admin"))
                    {
                        return RedirectToAction("Index", "Dashboard", null);
                    }
                    else if (await _userManager.IsInRoleAsync(user, "Customer"))
                    {
                        await LoadAsync(user);
                        return Page();
                    }

                    break;
            }
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
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
            user.UserFirstName = Input.FirstName;
            user.UserLastName = Input.LastName;
            user.PhoneNumber = Input.PhoneNumber;
            user.Email = Input.Email;
            user.UserName = Input.Email;
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
