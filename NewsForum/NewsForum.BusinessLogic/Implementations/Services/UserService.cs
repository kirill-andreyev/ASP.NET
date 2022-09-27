using Microsoft.EntityFrameworkCore;
using NewsForum.BusinessLogic.Interfaces.Services;
using NewsForum.BusinessLogic.Models;
using NewsForum.Database.Models.Models;
using NewsForum.Repositories;

namespace NewsForum.BusinessLogic.Implementations.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserBL> SingIn(UserBL user)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(a => a.Name == user.Name);

            if (dbUser == null || dbUser.Password != user.Password)
            {
                return null;
            }

            user.Role = dbUser.Role;
            return user;

        }

        public UserBL? CreateAccount(UserBL user)
        {
            var dbUser = _context.Users.FirstOrDefault(a => a.Name == user.Name);

            if (dbUser != null)
            {
                if (dbUser.Password == user.Password)
                {
                    user.Role = dbUser.Role;
                    return user;
                }
            }

            user.Role = "user";

            User tempUser = new User();

            tempUser.Name = user.Name;
            tempUser.Password = user.Password;
            tempUser.Role = user.Role;

            _context.Add(tempUser);
            _context.SaveChanges();

            return user;
        }

        public UserBL FindUser(int? id)
        {
            var dbUser = _context.Users.FirstOrDefaultAsync(a => a.Id == id);

            if (dbUser == null)
                throw new ArgumentNullException(nameof(dbUser));

            return new UserBL()
            {
                Name = dbUser.Result.Name
            };
        }
    }
}
