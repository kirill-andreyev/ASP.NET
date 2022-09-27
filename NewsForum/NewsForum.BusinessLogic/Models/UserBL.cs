using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsForum.BusinessLogic.Models
{
    public class UserBL
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string Role { get; set; }
        public IList<CommentBL>? Comments { get; set; }
    }
}
