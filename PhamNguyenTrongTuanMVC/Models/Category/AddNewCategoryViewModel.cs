using System.ComponentModel.DataAnnotations;
using RepositoryLayer.Enums;

namespace PhamNguyenTrongTuanMVC.Models.Category
{
    public class AddNewCategoryViewModel
    {
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
