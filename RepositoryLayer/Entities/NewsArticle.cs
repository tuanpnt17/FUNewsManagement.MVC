using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RepositoryLayer.Enums;

namespace RepositoryLayer.Entities
{
    public class NewsArticle
    {
        [Key]
        [StringLength(20)]
        public required string NewsArticleId { get; set; }

        [Required]
        [StringLength(400)]
        public required string NewsTitle { get; set; }

        [Required]
        [StringLength(150)]
        public required string Headline { get; set; }

        public DateTime CreatedDate { get; set; }

        [Column(TypeName = "text")]
        public string? NewsContent { get; set; }

        [StringLength(400)]
        public string? NewsSource { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public required NewsStatus NewsStatus { get; set; }

        // Foreign keys
        public int CategoryId { get; set; }
        public int CreatedById { get; set; }
        public int UpdatedById { get; set; }

        public DateTime? ModifiedDate { get; set; }

        // Navigation properties
        public virtual Category? Category { get; set; }
        public virtual SystemAccount? CreatedBy { get; set; }
        public virtual SystemAccount? UpdatedBy { get; set; }

        // Relationship to Tags via the join entity
        public virtual ICollection<NewsTag> NewsTags { get; set; } = [];
    }
}
