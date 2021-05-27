using System.ComponentModel.DataAnnotations;

namespace PL.Models
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Comment length must be between 1 and 100")]
        public string Text { get; set; }
        [Required]
        public string KindOfComment { get; set; }
        [Required]
        [Range(1, 10, ErrorMessage = "Mark must be between 1 and 10")]
        public int Mark { get; set; }
        public int? ClientId { get; set; }
    }
}