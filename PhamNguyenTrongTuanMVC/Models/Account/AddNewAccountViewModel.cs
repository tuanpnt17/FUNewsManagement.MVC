using System.ComponentModel.DataAnnotations;
using RepositoryLayer.Enums;

namespace PhamNguyenTrongTuanMVC.Models.Account
{
    public class AddNewAccountViewModel
    {
        [Display(Name = "Name")]
        [Required]
        [StringLength(50, MinimumLength = 3)]
        [DataType(DataType.Text)]
        public required string AccountName { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public required string AccountEmail { get; set; }

        [Display(Name = "Role")]
        [Required]
        public AccountRole AccountRole { get; set; }
    }
}
