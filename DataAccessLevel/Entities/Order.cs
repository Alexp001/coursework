using System;

namespace DataAccessLevel.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int? ClientId { get; set; }
        public int? EmployeeId { get; set; }
        public bool AccountingImplementation { get; set; }
        public Order(int id, DateTime date, int? clientId, int? employeeId, bool accountingImplementation)
        {
            Id = id;
            Date = date;
            ClientId = clientId;
            EmployeeId = employeeId;
            AccountingImplementation = accountingImplementation;
        }
    }
}
