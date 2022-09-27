using Microsoft.Extensions.DependencyInjection;
using NewsForum.BusinessLogic.Implementations.Services;
using NewsForum.BusinessLogic.Interfaces.Services;

namespace NewsForum.Repositories
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {
            services.AddTransient<IArticleService, ArticleService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IBlobStorageService, BlobStorageService>();

            return services;
        }
    }
}
