using System.ComponentModel.DataAnnotations;

namespace PL.Models
{
    public class EmployeeViewModel
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
        [Required]
        [Range(typeof(double), "100.00", "9999999.99", ErrorMessage = "Salary must be more 100 $")]
        public double Salary { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Position length must be between 1 and 20")]
        public string Position { get; set; }
        public int UserId { get; set; }
    }
}