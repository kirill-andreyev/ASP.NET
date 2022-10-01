using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewsForum.BusinessLogic.Interfaces.Services;
using NewsForum.BusinessLogic.Models;

namespace NewsForum.Pages.News.Comments
{
    public class DeleteModel : PageModel
    {
        private readonly ICommentService _commentService;

        public DeleteModel(ICommentService commentService)
        {
            _commentService = commentService;
        }


        [BindProperty] public CommentBL Comment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _commentService.GetComment(id);

            if (comment == null)
            {
                return NotFound();
            }

            Comment = comment;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value == Comment.UserId.ToString() || HttpContext.User.IsInRole("admin"))
            {
                await _commentService.DeleteComment(id);
            }
            else
            {
                return RedirectToPage("./WrongUser");
            }

            return RedirectToPage("./Index", new { id = Comment.ArticleId });
        }
    }
}