using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewsForum.BusinessLogic.Implementations.Services;
using NewsForum.BusinessLogic.Interfaces.Services;
using NewsForum.BusinessLogic.Models;
using NewsForum.Database.Models.Models;
using Microsoft.Extensions.Configuration;

namespace NewsForum.Pages.News
{
    public class IndexModel : PageModel
    {
        private readonly IArticleService _articleService;

        internal IList<ArticleBL> Articles { get; set; }

        public IndexModel(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public async Task OnGetAsync()
        {
            Articles = await _articleService.GetArticleList();

        }
    }
}