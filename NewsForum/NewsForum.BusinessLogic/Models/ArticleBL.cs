namespace NewsForum.BusinessLogic.Models
{
    public class ArticleBL
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? PathToImage { get; set; }
        public DateTime CreatedTime { get; set; }

        public IList<CommentBL>? Comments { get; set; }
    }
}