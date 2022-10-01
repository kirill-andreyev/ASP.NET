using System.Security.Authentication;
using Microsoft.EntityFrameworkCore;
using NewsForum.BusinessLogic.Interfaces.Services;
using NewsForum.BusinessLogic.Mapping;
using NewsForum.BusinessLogic.Models;
using NewsForum.Repositories;

namespace NewsForum.BusinessLogic.Implementations.Services
{
    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext _context;

        public CommentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CommentBL> GetComment(int? id)
        {
            return CommentMapper.MapToBLL(_context.Comments.Include(x => x.User).FirstOrDefault(a => a.Id == id));
        }

        public async Task DeleteComment(int? id)
        {
            var dbComment = _context.Comments.Include(x => x.User).FirstOrDefault(a => a.Id == id);
            _context.Comments.Remove(dbComment);
            _context.SaveChanges();
        }

        public async Task CreateComment(CommentBL comment)
        {
            var dbUser = _context.Users.FirstOrDefault(x => x.Name == comment.User.Name);
            if (dbUser == null)
            {
                return;
            }

            comment.UserId = dbUser.Id;

            _context.Comments.Add(CommentMapper.MapToDAL(comment));
            await _context.SaveChangesAsync();
        }

        public async Task UpdateComment(CommentBL comment)
        {
            var dbComment = _context.Comments.Include(x => x.User).FirstOrDefault(a => a.Id == comment.Id);

            if (dbComment == null)
            {
                return;
            }

            if (dbComment.UserId != comment.UserId)
            {
                throw new AuthenticationException();
            }

            dbComment.Text = comment.Text;
            dbComment.Title = comment.Title;

            await _context.SaveChangesAsync();
        }

        public async Task<IList<CommentBL>> GetCommentList(int? id)
        {
            return _context.Comments.Include(x => x.User).Where(x => x.ArticleId == id).Select(CommentMapper.MapToBLL).ToList();
        }
    }
}