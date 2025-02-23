namespace PhamNguyenTrongTuanMVC.Models.NewsArticle
{
    public class NewsArticleViewModel
    {
        public int NewsArticleId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateTime DatePublished { get; set; }
    }
}
