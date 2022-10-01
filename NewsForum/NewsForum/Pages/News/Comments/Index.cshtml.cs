using Microsoft.AspNetCore.Mvc.RazorPages;
using NewsForum.BusinessLogic.Interfaces.Services;
using NewsForum.BusinessLogic.Models;

namespace NewsForum.Pages.News.Comments
{
    public class IndexModel : PageModel
    {
        private readonly ICommentService _commentService;

        public IndexModel(ICommentService commentService)
        {
            _commentService = commentService;
        }

        internal IList<CommentBL> Comment { get; set; }

        public async Task OnGetAsync(int? id)
        {
            Comment = await _commentService.GetCommentList(id);
        }
    }
}