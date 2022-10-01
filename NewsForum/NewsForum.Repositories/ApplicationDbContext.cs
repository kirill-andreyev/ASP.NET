using Microsoft.EntityFrameworkCore;
using NewsForum.Database.Models.Models;

namespace NewsForum.Repositories
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Name = "admin",
                Password = "4813494d137e1631bba301d5acab6e7bb7aa74ce1185d456565ef51d737677b2", //root
                Role = "admin"
            });
        }
    }

}