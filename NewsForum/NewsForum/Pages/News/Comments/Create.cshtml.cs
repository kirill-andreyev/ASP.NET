﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewsForum.BusinessLogic.Interfaces.Services;
using NewsForum.BusinessLogic.Models;

namespace NewsForum.Pages.News.Comments
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ICommentService _commentService;

        public CreateModel(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [BindProperty] public CommentBL Comment { get; set; }

        public IActionResult OnGet(int id)
        {
            Comment = new CommentBL
            {
                ArticleId = id
            };
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            Comment.User = new UserBL();
            Comment.User.Name = HttpContext.User.Identity.Name;
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _commentService.CreateComment(Comment);
            return RedirectToPage("./Index", new { id = Comment.ArticleId });
        }
    }
}