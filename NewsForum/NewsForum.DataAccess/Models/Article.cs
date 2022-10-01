namespace NewsForum.Database.Models.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? PathToImage { get; set; }
        public DateTime CreatedTime { get; set; }
        public IList<Comment>? Comments { get; set; }
    }
}