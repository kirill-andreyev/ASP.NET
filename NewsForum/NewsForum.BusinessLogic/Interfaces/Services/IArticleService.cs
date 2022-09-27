using NewsForum.BusinessLogic.Models;
using NewsForum.Database.Models.Models;

namespace NewsForum.BusinessLogic.Interfaces.Services
{
    public interface IArticleService
    {
        Task<IList<ArticleBL>> GetArticleList();
        Task CreateArticle(ArticleBL article);
        Task UpdateArticle(ArticleBL article);
        Task<ArticleBL> GetArticle(int? id);
        Task DeleteArticle(int? id);
    }
}
