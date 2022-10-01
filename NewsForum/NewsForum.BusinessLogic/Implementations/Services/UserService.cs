using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using NewsForum.BusinessLogic.Interfaces.Services;
using NewsForum.BusinessLogic.Models;
using NewsForum.Database.Models.Models;
using NewsForum.Repositories;
using System.Text;
using Azure.Identity;

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

            user.Password = GenerateSHA256(user.Password);

            if (dbUser == null || dbUser.Password != user.Password)
            {
                throw new AuthenticationFailedException("Wrong Password");
            }

            user.Role = dbUser.Role;
            user.Id = dbUser.Id;
            return user;
        }

        public async Task CreateAccount(UserBL user)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(a => a.Name == user.Name);

            if (dbUser != null)
            {
                throw new AuthenticationFailedException("User already exist");
            }

            user.Role = "user";

            var tempUser = new User();

            tempUser.Name = user.Name;
            tempUser.Password = GenerateSHA256(user.Password);
            tempUser.Role = user.Role;

            await _context.AddAsync(tempUser);
            await _context.SaveChangesAsync();
        }

        public UserBL FindUser(int? id)
        {
            var dbUser = _context.Users.FirstOrDefaultAsync(a => a.Id == id);

            if (dbUser == null)
            {
                throw new ArgumentNullException(nameof(dbUser));
            }

            return new UserBL
            {
                Name = dbUser.Result.Name
            };
        }

        public string GenerateSHA256(string password)
        {
            var hash = SHA256.Create();
            var sourceBytes = Encoding.UTF8.GetBytes(password);
            var hashBytes = hash.ComputeHash(sourceBytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}