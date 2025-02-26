using System.ComponentModel.DataAnnotations;

namespace PhamNguyenTrongTuanMVC.Models.Account
{
    public class UpdateProfileViewModel
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

        [DataType(DataType.Password)]
        public string? AccountPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare(
            "AccountPassword",
            ErrorMessage = "The password and confirmation password do not match."
        )]
        public string? ConfirmPassword { get; set; }
    }
}
