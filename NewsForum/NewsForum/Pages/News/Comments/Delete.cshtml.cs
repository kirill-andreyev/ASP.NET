using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NewsForum.BusinessLogic.Interfaces.Services;
using NewsForum.BusinessLogic.Models;
using NewsForum.Database.Models.Models;
using NewsForum.Repositories;

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
            else
            {
                Comment = comment;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (HttpContext.User.Identity.Name == Comment.User.Name || HttpContext.User.IsInRole("admin"))
            {
                await _commentService.DeleteComment(id);
            }
            else
            {
                return RedirectToPage("./WrongUser");
            }

            return RedirectToPage($"./Index", new { id = Comment.ArticleId });
        }
    }
}