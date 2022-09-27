using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsForum.BusinessLogic.Models;
using NewsForum.Database.Models.Models;

namespace NewsForum.BusinessLogic.Interfaces.Services
{
    public interface IUserService
    {
        public Task<UserBL> SingIn(UserBL user);
        public UserBL? CreateAccount(UserBL user);
        public UserBL FindUser(int? id);
    }
}
