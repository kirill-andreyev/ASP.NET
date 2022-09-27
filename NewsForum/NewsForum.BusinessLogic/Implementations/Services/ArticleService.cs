using Microsoft.EntityFrameworkCore;
using NewsForum.BusinessLogic.Interfaces.Services;
using NewsForum.BusinessLogic.Mapping;
using NewsForum.BusinessLogic.Models;
using NewsForum.Database.Models.Models;
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
            var articlesDb = await _context.Articles.ToListAsync();
            return articlesDb.Select(ArticleMapper.MapToBLL).ToList();
        }

        public async Task CreateArticle(ArticleBL article)
        {
            _context.Articles.Add(ArticleMapper.MapToDAL(article));
            await _context.SaveChangesAsync();
        }

        public async Task UpdateArticle(ArticleBL article)
        {
            var dbArticle = _context.Articles.FirstOrDefault(x => x.Id == article.Id);

            if (dbArticle == null)
            {
                return;
            }
            
            dbArticle.CreatedTime = DateTime.UtcNow;
            dbArticle.Description = article.Description;
            dbArticle.Title = article.Title;
            dbArticle.PathToImage = article.PathToImage;

            await _context.SaveChangesAsync();
        }

        public async Task<ArticleBL> GetArticle(int? id)
        {
           return ArticleMapper.MapToBLL(_context.Articles.FirstOrDefault(a => a.Id == id));
        }

        public async Task DeleteArticle(int? id)
        {
            var article = _context.Articles.FirstOrDefault(x => x.Id == id);
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
        }
    }
}