using System;
using System.Collections.Generic;

namespace PL.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool AccountingImplementation { get; set; }
        public ClientViewModel Client { get; set; }
        public EmployeeViewModel Employee { get; set; }
        public IEnumerable<PizzaAccountingViewModel> Pizzas { get; set; }
    }
}