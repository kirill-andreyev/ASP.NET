using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewsForum.BusinessLogic.Interfaces.Services;
using NewsForum.BusinessLogic.Models;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication;


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

            User.Password = GenerateSHA256(User.Password);
            User = await _userService.SingIn(User);

            if (User == null)
            {
                return Page();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, User.Name),
                new Claim(ClaimTypes.Role, User.Role)
            };

            var claimsIndentity = new ClaimsIdentity(claims, "Cookies");
            var claimsPrincipal = new ClaimsPrincipal(claimsIndentity);

            await HttpContext.SignInAsync(claimsPrincipal);

            return RedirectToPage("./Index");
        }

        public string GenerateSHA256(string password)
        {
            SHA256 hash = SHA256.Create();
            byte[] sourceBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = hash.ComputeHash(sourceBytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
