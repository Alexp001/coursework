using System;
using System.ComponentModel.DataAnnotations;

namespace PL.Models
{
    [Serializable]
    public class PizzaAccountingViewModel
    {
        public PizzaViewModel PizzaObject { get; set; }
        [Required]
        [Range(1, 100, ErrorMessage = "Quantity must be between 1 and 100")]
        public int Quantity { get; set; }
        public override string ToString()
        {
            return PizzaObject.Name + " " + PizzaObject.Price + "$ " + " " + Quantity;
        }
    }
}