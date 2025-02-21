using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FUNewsManagementSystem.RepositoryLayer.Enums;

namespace FUNewsManagementSystem.RepositoryLayer.Entities
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(100)]
        public required string CategoryName { get; set; }

        [StringLength(250)]
        public required string CategoryDescription { get; set; }

        [NotMapped]
        public required CategoryStatus CategoryStatus { get; set; }

        public int? ParentCategoryId { get; set; }
        public virtual Category? ParentCategory { get; set; }
        public virtual ICollection<Category>? SubCategories { get; set; }

        // Navigation property
        public virtual ICollection<NewsArticle>? NewsArticles { get; set; }
    }
}
