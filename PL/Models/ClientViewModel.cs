using System.ComponentModel.DataAnnotations;

namespace PL.Models
{
    public class ClientViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Name length must be between 1 and 20")]
        [RegularExpression(@"^[A-zА-я ]+$", ErrorMessage = "Name must consist of letters")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Address length must be between 1 and 20")]
        public string Address { get; set; }
        public int UserId { get; set; }
    }
}