namespace PhamNguyenTrongTuanMVC.Models
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }

        public required string CategoryName { get; set; }
        public int NewsArticleCount { get; set; }
    }
}
