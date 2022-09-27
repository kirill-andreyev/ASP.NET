using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewsForum.BusinessLogic.Interfaces.Services;
using NewsForum.BusinessLogic.Models;

namespace NewsForum.Pages.News
{
    [Authorize(Roles = "admin")]
    public class CreateModel : PageModel
    {
        private readonly IArticleService _articleService;
        private readonly IBlobStorageService _blobStorageService;

        public CreateModel(IArticleService articleService, IBlobStorageService blobStorageService)
        {
            _articleService = articleService;
            _blobStorageService = blobStorageService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty] public ArticleBL Article { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            IFormFileCollection files = Request.Form.Files;
            var file = files[0];
            var stream = file.OpenReadStream();

            await _blobStorageService.UploadToBlobStorage(stream, file.FileName);

            Article.PathToImage = file.FileName;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _articleService.CreateArticle(Article);

            return RedirectToPage("./Index");
        }
    }
}
