using System.Drawing;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewsForum.BusinessLogic.Interfaces.Services;
using NewsForum.BusinessLogic.Models;

namespace NewsForum.Pages.News
{
    public class DetailsModel : PageModel
    {
        private readonly IArticleService _articleService;
        public readonly IBlobStorageService _blobStorageService;

        public DetailsModel(IArticleService articleService, IBlobStorageService blobStorageService)
        {
            _articleService = articleService;
            _blobStorageService = blobStorageService;
        }

        public ArticleBL Article { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Article = await _articleService.GetArticle(id);

            if (Article == null)
            {
                return NotFound();
            }
            
            return Page();
        }
    }
}
