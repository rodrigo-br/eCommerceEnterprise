using FluentValidation;
using FluentValidation.Results;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECE.Cart.API.Models
{
    public class CustomerCart
    {
        internal const int MAX_PRODUCT_AMOUNT = 20;

        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public decimal TotalValue { get; set; }
        public List<ProductCart> Products { get; set; } = new List<ProductCart>();
        [NotMapped]
        public ValidationResult? ValidationResult { get; set; }

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

        internal void UpdateProduct(ProductCart product)
        {
            product.LinkCart(Id);

            var existingProduct = GetProductById(product.ProductId);

            Products.Remove(existingProduct);
            Products.Add(product);

            ComputeTotalCartValue();
        }

        internal void UpdateAmount(ProductCart product, int amount)
        {
            product.UpdateProductAmount(amount);
            UpdateProduct(product);
        }

        internal void DeleteProduct(ProductCart product)
        {
            var existingProduct = GetProductById(product.ProductId);
            Products.Remove(existingProduct);

            ComputeTotalCartValue();
        }

        internal bool IsValid()
        {
            var errors = Products.SelectMany(p => 
                new ProductCart.OrderedProductValidation()
                    .Validate(p).Errors).ToList();
            errors.AddRange(new CustomerCartValidation().Validate(this).Errors);

            ValidationResult = new ValidationResult(errors);

            return ValidationResult.IsValid;
        }

        public class CustomerCartValidation : AbstractValidator<CustomerCart>
        {
            public CustomerCartValidation()
            {
                RuleFor(c => c.CustomerId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Customer not recognized");

                RuleFor(c => c.Products.Count)
                    .GreaterThan(0)
                    .WithMessage("The cart is empty");

                RuleFor(c => c.TotalValue)
                    .GreaterThan(0)
                    .WithMessage("The total value must be greater than 0");
            }
        }

    }
}
