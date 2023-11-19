namespace ECE.ApiGateway.Purchases.Models
{
    public class CartDTO
    {
        public decimal TotalValue { get; set; }
        public decimal Discount { get; set; }
        public List<ProductCartDTO> Products { get; set; } = new List<ProductCartDTO>();
    }
}
