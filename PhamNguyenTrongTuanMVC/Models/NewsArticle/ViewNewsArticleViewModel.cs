using System.ComponentModel.DataAnnotations;
using RepositoryLayer.Entities;
using RepositoryLayer.Enums;

namespace PhamNguyenTrongTuanMVC.Models.NewsArticle
{
    public class ViewNewsArticleViewModel
    {
        [Display(Name = "Id")]
        public required string NewsArticleId { get; set; }

        [Display(Name = "Title")]
        public required string NewsTitle { get; set; }

        [Display(Name = "Headline")]
        public required string Headline { get; set; }

        public string? NewsSource { get; set; }

        [Display(Name = "Status")]
        public required NewsStatus NewsStatus { get; set; }

        [Display(Name = "Date Modified")]
        public DateTime ModifiedDate { get; set; }

        [Display(Name = "Category")]
        public string? CategoryName { get; set; }

        [Display(Name = "Author")]
        public string? CreatedByName { get; set; }

        [Display(Name = "Updated By")]
        public string? UpdatedByName { get; set; }

        public virtual ICollection<NewsTag>? NewsTags { get; set; }
    }
}
