using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewsForum.BusinessLogic.Interfaces.Services;
using NewsForum.BusinessLogic.Models;

namespace NewsForum.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;

        public LoginModel(IUserService userService)
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
                await _userService.SingIn(User);
            }
            catch (Exception e)
            {
                return RedirectToPage("./LoginFail", new {error = e.ToString()});
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, User.Name),
                new(ClaimTypes.Role, User.Role),
                new(ClaimTypes.NameIdentifier, User.Id.ToString())
            };

            var claimsIndentity = new ClaimsIdentity(claims, "Cookies");
            var claimsPrincipal = new ClaimsPrincipal(claimsIndentity);

            await HttpContext.SignInAsync(claimsPrincipal);

            return RedirectToPage("./Index");
        }
    }
}