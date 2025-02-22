using RepositoryLayer.Enums;

namespace ServiceLayer.Models
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }

        public required string CategoryName { get; set; }

        public required string CategoryDescription { get; set; }

        public required CategoryStatus CategoryStatus { get; set; }

        public int? ParentCategoryId { get; set; }
        public virtual CategoryDTO? ParentCategory { get; set; }
        public virtual ICollection<CategoryDTO>? SubCategories { get; set; }

        public virtual ICollection<NewsArticleDTO> NewsArticles { get; set; }
    }
}
