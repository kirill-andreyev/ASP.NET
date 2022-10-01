namespace NewsForum.BusinessLogic.Models
{
    public class CommentBL
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public int ArticleId { get; set; }
        public ArticleBL? Article { get; set; }
        public int UserId { get; set; }
        public UserBL? User { get; set; }
    }
}