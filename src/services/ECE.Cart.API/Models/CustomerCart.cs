namespace ECE.Cart.API.Models
{
    public class CustomerCart
    {
        internal const int MAX_PRODUCT_AMOUNT = 20;

        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public decimal TotalValue { get; set; }
        public List<ProductCart> Products { get; set; } = new List<ProductCart>();

        public CustomerCart() { }

        public CustomerCart(Guid customerId)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
        }

        internal void ComputeTotalCartValue()
        {
            TotalValue = Products.Sum(p => p.ComputeValue());
        }

        internal bool ExistingProductCart(ProductCart product)
        {
            return Products.Any(p => p.ProductId == product.ProductId);
        }

        internal ProductCart GetProductById(Guid productId)
        {
            return Products.FirstOrDefault(p => p.ProductId == productId);
        }

        internal void AddProduct(ProductCart product)
        {
            if (!product.IsValid()) return;

            product.LinkCart(Id);

            if (ExistingProductCart(product))
            {
                var existingProduct = GetProductById(product.ProductId);
                existingProduct.ChangeProductAmount(product.ProductAmount);

                product = existingProduct;
                Products.Remove(existingProduct);
            }

            Products.Add(product);
            ComputeTotalCartValue();
        }

    }
}
