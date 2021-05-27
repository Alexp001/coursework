using System;
using System.ComponentModel.DataAnnotations;

namespace PL.Models
{
    [Serializable]
    public class PizzaViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Name length must be between 1 and 20")]
        [RegularExpression(@"^[A-zА-я ]+$", ErrorMessage = "Name must consist of letters")]
        public string Name { get; set; }
        [Required]
        [Range(typeof(double), "00.00", "9999999.99", ErrorMessage = "Salary must be more 0 $")]
        public double Price { get; set; }
        [Required]
        [Url]
        public string Image { get; set; }
    }
}