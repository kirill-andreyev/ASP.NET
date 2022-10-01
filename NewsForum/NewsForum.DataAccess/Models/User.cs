﻿namespace NewsForum.Database.Models.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string Role { get; set; }
        public IList<Comment>? Comments { get; set; }
    }
}