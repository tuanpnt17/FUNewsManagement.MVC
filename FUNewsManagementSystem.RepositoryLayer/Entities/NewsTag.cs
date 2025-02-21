using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FUNewsManagementSystem.RepositoryLayer.Entities
{
    public class NewsTag
    {
        [Key, Column(Order = 0)]
        [StringLength(20)]
        public required string NewsArticleId { get; set; }

        [Key, Column(Order = 1)]
        public int TagId { get; set; }

        // Navigation properties
        public virtual NewsArticle? NewsArticle { get; set; }
        public virtual Tag? Tag { get; set; }
    }
}
