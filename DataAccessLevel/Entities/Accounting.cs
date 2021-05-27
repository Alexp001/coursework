namespace DataAccessLevel.Entities
{
    public class Accounting
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int PizzaId { get; set; }
        public int OrderId { get; set; }
        public Accounting(int id, int quantity, int pizzaId, int orderId)
        {
            Id = id;
            Quantity = quantity;
            PizzaId = pizzaId;
            OrderId = orderId;
        }
    }
}
