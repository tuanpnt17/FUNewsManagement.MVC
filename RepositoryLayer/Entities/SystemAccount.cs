using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RepositoryLayer.Enums;

namespace RepositoryLayer.Entities
{
    public class SystemAccount
    {
        [Key]
        public int AccountId { get; set; }

        [Required]
        [StringLength(100)]
        public required string AccountName { get; set; }

        [Required]
        [StringLength(70)]
        public required string AccountEmail { get; set; }

        [Required]
        [Column("AccountRole", TypeName = "int")]
        public required AccountRole AccountRole { get; set; }

        [Required]
        [StringLength(200)]
        public required string AccountPassword { get; set; }

        // Navigation properties
        public virtual ICollection<NewsArticle>? CreatedArticles { get; set; }
        public virtual ICollection<NewsArticle>? UpdatedArticles { get; set; }
    }
}
