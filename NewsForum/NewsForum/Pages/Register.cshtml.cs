using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewsForum.BusinessLogic.Interfaces.Services;
using NewsForum.BusinessLogic.Models;

namespace NewsForum.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IUserService _userService;

        public RegisterModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty] public UserBL User { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (User.Name == null || User.Password == null)
            {
                return Page();
            }

            try
            {
                await _userService.CreateAccount(User);
            }
            catch (Exception e)
            {
                RedirectToPage("./WrongUserName");
            }

            var claims = new List<Claim>
            {
                new(ClaimsIdentity.DefaultNameClaimType, User.Name),
                new(ClaimsIdentity.DefaultRoleClaimType, User.Role)
            };

            var claimsIndentity = new ClaimsIdentity(claims, "Cookies");
            var claimsPrincipal = new ClaimsPrincipal(claimsIndentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            return RedirectToPage("./Index");
        }
    }
}