namespace ECE.Cart.API.Models
{
    public class CustomerCart
    {
        public CustomerCart() { }

        public CustomerCart(Guid customerId)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
        }

        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public decimal TotalValue { get; set; }
        public List<ProductCart> Products { get; set; } = new List<ProductCart>();
    }
}
