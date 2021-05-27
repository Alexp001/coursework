namespace DataAccessLevel.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public double Salary { get; set; }
        public string Position { get; set; }
        public string Address { get; set; }
        public int UserId { get; set; }
        public Employee(int id, string name, string email, string phone, double salary, string position, string address, int userId)
        {
            Id = id;
            Name = name;
            Email = email;
            Phone = phone;
            Salary = salary;
            Position = position;
            Address = address;
            UserId = userId;
        }
    }
}
