using FluentValidation;

namespace ECE.Cart.API.Models
{
    public class ProductCart
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductAmount { get; set; }
        public decimal ProductValue { get; set; }
        public string ProductImage { get; set; }

        // Nav
        public Guid CartId { get; set; }
        public CustomerCart CustomerCart { get; set; }

        public ProductCart()
        {
            Id = Guid.NewGuid();
        }

        internal void LinkCart(Guid cartId)
        {
            CartId = cartId;
        }

        internal decimal ComputeValue()
        {
            return ProductAmount * ProductValue;
        }

        internal void ChangeProductAmount(int amount)
        {
            ProductAmount += amount;
        }

        internal bool IsValid()
        {
            return new OrderedProductValidation().Validate(this).IsValid;
        }

        public class OrderedProductValidation : AbstractValidator<ProductCart>
        {
            public OrderedProductValidation()
            {
                RuleFor(c => c.ProductId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Invalid product Id");

                RuleFor(c => c.ProductName)
                    .NotEmpty()
                    .WithMessage("Product name is empty");

                RuleFor(c => c.ProductAmount)
                    .GreaterThan(0)
                    .WithMessage("The minimum product amount is 1");

                RuleFor(c => c.ProductAmount)
                    .LessThan(CustomerCart.MAX_PRODUCT_AMOUNT)
                    .WithMessage($"The maximum product amount is {CustomerCart.MAX_PRODUCT_AMOUNT}");

                RuleFor(c => c.ProductValue)
                    .GreaterThan(0)
                    .WithMessage("The product value must be greater than 0");
            }
        }

    }
}
