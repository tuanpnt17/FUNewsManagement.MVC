using System.ComponentModel.DataAnnotations;

namespace FUNewsManagementSystem.RepositoryLayer.Entities
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }

        [Required]
        [StringLength(50)]
        public required string TagName { get; set; }

        [Required]
        [StringLength(400)]
        public required string Note { get; set; }

        // Navigation property
        public virtual ICollection<NewsTag>? NewsTags { get; set; }
    }
}
