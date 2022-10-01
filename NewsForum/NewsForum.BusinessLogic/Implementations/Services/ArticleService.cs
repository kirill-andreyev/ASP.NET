using Microsoft.EntityFrameworkCore;
using NewsForum.BusinessLogic.Interfaces.Services;
using NewsForum.BusinessLogic.Mapping;
using NewsForum.BusinessLogic.Models;
using NewsForum.Repositories;

namespace NewsForum.BusinessLogic.Implementations.Services
{
    public class ArticleService : IArticleService
    {
        private readonly ApplicationDbContext _context;

        public ArticleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IList<ArticleBL>> GetArticleList()
        {
            // var articlesDb = await _context.Articles.ToListAsync();
            return _context.Articles.Select(ArticleMapper.MapToBLL).ToList();
        }

        public async Task<int> CreateArticle(ArticleBL article)
        {
            var entity = await _context.Articles.AddAsync(ArticleMapper.MapToDAL(article));
            await _context.SaveChangesAsync();
            return entity.Entity.Id;
        }

        public async Task UpdateArticle(ArticleBL article)
        {
            var dbArticle = await _context.Articles.FirstOrDefaultAsync(x => x.Id == article.Id);

            if (dbArticle == null)
            {
                throw new Exception("Article doesn't exist");
            }

            dbArticle.CreatedTime = DateTime.UtcNow;
            dbArticle.Description = article.Description;
            dbArticle.Title = article.Title;
            dbArticle.PathToImage = article.PathToImage;

            await _context.SaveChangesAsync();
        }

        public async Task<ArticleBL> GetArticle(int id)
        {
            var article = await _context.Articles.FirstOrDefaultAsync(a => a.Id == id);

            if (article == null)
            {
                throw new Exception("Article doesn't exist");
            }

            return ArticleMapper.MapToBLL(article);
        }

        public async Task DeleteArticle(int? id)
        {
            var article = _context.Articles.FirstOrDefault(x => x.Id == id);

            if (article == null)
            {
                throw new Exception("Article doesn't exist");
            }

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
        }
    }
}