namespace DataAccessLevel.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string KindOfComment { get; set; }
        public int Mark { get; set; }
        public int? ClientId { get; set; }
        public Comment(int id, string text, string kindOfComment, int mark, int? clientId)
        {
            Id = id;
            Text = text;
            KindOfComment = kindOfComment;
            Mark = mark;
            ClientId = clientId;
        }
    }
}
