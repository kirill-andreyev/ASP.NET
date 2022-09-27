using Common.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace NewsForum.Repositories
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMigrationsDll(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString, x => x.MigrationsAssembly(ConstantsStrings.MigrationsAssemblyName));
            });

            return services;
        }
    }
}
