using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewsForum.BusinessLogic.Interfaces.Services;
using NewsForum.BusinessLogic.Models;

namespace NewsForum.Pages.News
{
    [Authorize(Roles = "admin")]
    public class EditModel : PageModel
    {
        private readonly IArticleService _articleService;

        public EditModel(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [BindProperty] public ArticleBL Article { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Article = await _articleService.GetArticle(id);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _articleService.UpdateArticle(Article);

            return RedirectToPage("./Index");
        }
    }
}