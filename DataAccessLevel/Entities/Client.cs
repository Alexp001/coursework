namespace DataAccessLevel.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int UserId { get; set; }
        public Client(int id, string name, string email, string phone, string address, int userId)
        {
            Id = id;
            Name = name;
            Email = email;
            Phone = phone;
            Address = address;
            UserId = userId;
        }
    }
}
