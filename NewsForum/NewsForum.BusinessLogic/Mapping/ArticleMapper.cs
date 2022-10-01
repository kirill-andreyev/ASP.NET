using NewsForum.BusinessLogic.Models;
using NewsForum.Database.Models.Models;

namespace NewsForum.BusinessLogic.Mapping
{
    public static class ArticleMapper
    {
        public static Article MapToDAL(ArticleBL articleBl, DateTime? createdTime = null)
        {
            return new Article
            {
                Id = articleBl.Id,
                Description = articleBl.Description,
                CreatedTime = createdTime ?? DateTime.UtcNow,
                PathToImage = articleBl.PathToImage,
                Title = articleBl.Title
            };
        }

        public static ArticleBL MapToBLL(Article article)
        {
            if (article == null)
            {
                throw new ArgumentNullException(nameof(article));
            }

            return new ArticleBL
            {
                Id = article.Id,
                Description = article.Description,
                CreatedTime = article.CreatedTime,
                PathToImage = article.PathToImage,
                Title = article.Title
            };
        }
    }
}