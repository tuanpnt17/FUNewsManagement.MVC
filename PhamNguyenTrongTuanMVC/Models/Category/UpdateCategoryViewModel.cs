using System.ComponentModel.DataAnnotations;
using RepositoryLayer.Enums;

namespace PhamNguyenTrongTuanMVC.Models.Category
{
    public class UpdateCategoryViewModel
    {
        [Required]
        public int CategoryId { get; set; }

        [Display(Name = "Category Name")]
        [Required]
        public required string CategoryName { get; set; }

        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        [Required]
        public required string CategoryDescription { get; set; }

        [Display(Name = "Status")]
        [Required]
        public required CategoryStatus CategoryStatus { get; set; }
    }
}
