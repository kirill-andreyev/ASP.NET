using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NewsForum.BusinessLogic.Interfaces.Services;
using NewsForum.BusinessLogic.Models;
using NewsForum.Database.Models.Models;
using NewsForum.Repositories;

namespace NewsForum.Pages.News.Comments
{
    public class DetailsModel : PageModel
    {
        private readonly ICommentService _commentService;

        public DetailsModel(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public CommentBL Comment { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            var comment = await _commentService.GetComment(id);

            Comment = comment;
            return Page();
        }
    }
}