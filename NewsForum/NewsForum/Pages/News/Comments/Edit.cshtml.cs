using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewsForum.BusinessLogic.Interfaces.Services;
using NewsForum.BusinessLogic.Models;
using NewsForum.Database.Models.Models;
using NewsForum.Repositories;

namespace NewsForum.Pages.News.Comments
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly ICommentService _commentService;

        public EditModel(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [BindProperty]
        public CommentBL Comment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Comment = await _commentService.GetComment(id);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (HttpContext.User.Identity.Name == Comment.User.Name)
            {
                await _commentService.UpdateComment(Comment);

            }
            else
            {
                return RedirectToPage("./WrongUser");
            }


            int id;
            return RedirectToPage($"./Index", new { id = Comment.ArticleId });
        }


    }
}