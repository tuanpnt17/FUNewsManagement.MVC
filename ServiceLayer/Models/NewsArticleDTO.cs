using RepositoryLayer.Entities;
using RepositoryLayer.Enums;

namespace ServiceLayer.Models;

public class NewsArticleDTO
{
    public required string NewsArticleId { get; set; }

    public required string NewsTitle { get; set; }

    public required string Headline { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? NewsContent { get; set; }

    public string? NewsSource { get; set; }

    public required NewsStatus NewsStatus { get; set; }

    public DateTime ModifiedDate { get; set; }

    public int CategoryId { get; set; }
    public int CreatedById { get; set; }
    public int UpdatedById { get; set; }

    public IEnumerable<int> TagIds { get; set; } = [];

    // Navigation properties
    public virtual CategoryDTO Category { get; set; } = null!;
    public virtual SystemAccount CreatedBy { get; set; } = null!;
    public virtual SystemAccount UpdatedBy { get; set; } = null!;

    // Relationship to Tags via the join entity
    public virtual ICollection<NewsTag> NewsTags { get; set; } = [];
}
