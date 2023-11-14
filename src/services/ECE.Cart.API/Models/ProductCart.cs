namespace ECE.Cart.API.Models
{
    public class ProductCart
    {
        public ProductCart()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductAmount { get; set; }
        public decimal ProductValue { get; set; }
        public string ProductImage { get; set; }

        // Nav
        public Guid CartId { get; set; }
        public CustomerCart CustomerCart { get; set; }

    }
}
