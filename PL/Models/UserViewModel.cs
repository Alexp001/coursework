using System;
using System.ComponentModel.DataAnnotations;

namespace PL.Models
{
    [Serializable]
    public class UserViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Login length must be between 4 and 20")]
        [RegularExpression(@"^[A-z@]+$", ErrorMessage = "Login must consist of letters")]
        public string Login { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Password length must be between 4 and 20")]
        public string Password { get; set; }
    }
}