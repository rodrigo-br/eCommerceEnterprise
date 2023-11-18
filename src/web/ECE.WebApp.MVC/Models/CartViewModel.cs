namespace ECE.WebApp.MVC.Models
{
    public class ProductCartViewModel
    {
        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }
        public int ProductAmount { get; set; }
        public decimal ProductValue { get; set; }
        public string? Image { get; set; }
    }

    public class CustomerCartViewModel
    {
        public decimal TotalValue { get; set; }
        public List<ProductCartViewModel> Products { get; set; } = new List<ProductCartViewModel>();
    }
}
