using System.ComponentModel.DataAnnotations;

namespace PL.Models
{
    public class ChangePasswordViewModel
    {
        [Required]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Password length must be between 4 and 20")]
        public string OldPassword { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Password length must be between 4 and 20")]
        public string NewPassword { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Password length must be between 4 and 20")]
        [Compare("NewPassword", ErrorMessage = "Password mismatch")]
        public string ConfirmPassword { get; set; }
    }
}