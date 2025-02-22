using System.ComponentModel.DataAnnotations;

namespace PhamNguyenTrongTuanMVC.Models.Account;

public class LoginAccountViewModel
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    [Display(Name = "Email or Username")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public required string Password { get; set; }
}
