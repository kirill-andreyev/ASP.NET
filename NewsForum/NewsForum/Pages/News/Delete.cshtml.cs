using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewsForum.BusinessLogic.Interfaces.Services;
using NewsForum.BusinessLogic.Models;

namespace NewsForum.Pages.News
{
    public class DeleteModel : PageModel
    {
        private readonly IArticleService _articleService;

        public DeleteModel(IArticleService articleService)
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

            var article = await _articleService.GetArticle(id);

            if (article == null)
            {
                return NotFound();
            }

            Article = article;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _articleService.DeleteArticle(id);


            return RedirectToPage("./Index");
        }
    }
}