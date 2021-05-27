namespace BLL.DTO
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string KindOfComment { get; set; }
        public int Mark { get; set; }
        public int? ClientId { get; set; }
    }
}
