using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsForum.BusinessLogic.Models;

namespace NewsForum.BusinessLogic.Interfaces.Services
{
    public interface ICommentService
    {
        Task<CommentBL> GetComment(int? id);
        Task DeleteComment(int? id);
        Task CreateComment(CommentBL comment);
        Task UpdateComment(CommentBL comment);
        Task<IList<CommentBL>> GetCommentList(int? id);
    }
}
