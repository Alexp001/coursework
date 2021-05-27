using System;

namespace BLL.DTO
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int? ClientId { get; set; }
        public int? EmployeeId { get; set; }
        public bool AccountingImplementation { get; set; }
    }
}
