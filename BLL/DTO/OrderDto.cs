using System;
using System.Collections.Generic;

namespace BLL.DTO
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool AccountingImplementation { get; set; }
        public ClientDto Client { get; set; }
        public EmployeeDto Employee { get; set; }
        public IEnumerable<PizzaAccountingDto> Pizzas { get; set; }
    }
}
