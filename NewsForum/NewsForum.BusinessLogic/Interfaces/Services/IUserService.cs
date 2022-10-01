using NewsForum.BusinessLogic.Models;

namespace NewsForum.BusinessLogic.Interfaces.Services
{
    public interface IUserService
    {
        public Task<UserBL> SingIn(UserBL user);
        public Task CreateAccount(UserBL user);
        public UserBL FindUser(int? id);
        public string GenerateSHA256(string password);
    }
}