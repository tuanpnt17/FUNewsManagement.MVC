using System.ComponentModel.DataAnnotations;
using RepositoryLayer.Enums;

namespace PhamNguyenTrongTuanMVC.Models.Account
{
    public class UpdateAccountViewModel
    {
        [Required]
        [Display(Name = "Account Id")]
        public int AccountId { get; set; }

        [Display(Name = "Name")]
        [Required]
        public string AccountName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string AccountEmail { get; set; } = string.Empty;

        [Display(Name = "Role")]
        [Required]
        public AccountRole AccountRole { get; set; }
    }
}
