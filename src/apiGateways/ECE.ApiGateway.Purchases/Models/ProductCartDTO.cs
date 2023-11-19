namespace ECE.ApiGateway.Purchases.Models
{
    public class ProductCartDTO
    {
        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }
        public int ProductAmount { get; set; }
        public decimal ProductValue { get; set; }
        public string? Image { get; set; }
    }
}
