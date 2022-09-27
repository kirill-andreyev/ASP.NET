using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsForum.BusinessLogic.Implementations.Services;
using NewsForum.BusinessLogic.Interfaces.Services;
using NewsForum.BusinessLogic.Models;
using NewsForum.Database.Models.Models;

namespace NewsForum.BusinessLogic.Mapping
{
    internal class CommentMapper
    {
        public static CommentBL MapToBLL(Comment comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }

            return new CommentBL()
            {
                Id = comment.Id,
                Title = comment.Title,
                Text = comment.Text,
                ArticleId = comment.ArticleId,
                UserId = comment.UserId,
                User = new UserBL()
                {
                    Name = comment.User.Name
                }
            };
        }

        public static Comment MapToDAL(CommentBL comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }

            return new Comment()
            {
                Id = comment.Id,
                Title = comment.Title,
                Text = comment.Text,
                ArticleId = comment.ArticleId,
                UserId = comment.UserId
            };
        }
    }
}
