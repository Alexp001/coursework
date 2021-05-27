namespace DataAccessLevel.Entities
{
    public class AccountingRoles
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public AccountingRoles(int id, int userId, int roleId)
        {
            Id = id;
            UserId = userId;
            RoleId = roleId;
        }
    }
}
