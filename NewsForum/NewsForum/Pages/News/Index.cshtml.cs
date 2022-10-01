using Microsoft.AspNetCore.Mvc.RazorPages;
using NewsForum.BusinessLogic.Interfaces.Services;
using NewsForum.BusinessLogic.Models;

namespace NewsForum.Pages.News
{
    public class IndexModel : PageModel
    {
        private readonly IArticleService _articleService;

        public IndexModel(IArticleService articleService)
        {
            _articleService = articleService;
        }

        internal IList<ArticleBL> Articles { get; set; }

        public async Task OnGetAsync()
        {
            Articles = await _articleService.GetArticleList();
        }
    }
}