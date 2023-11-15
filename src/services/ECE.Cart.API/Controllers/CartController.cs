using ECE.Cart.API.Data;
using ECE.Cart.API.Models;
using ECE.WebApi.Core.Controllers;
using ECE.WebApi.Core.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECE.Cart.API.Controllers
{
    [Authorize]
    public class CartController : MainController
    {
        private readonly IAspNetUser _aspNetUser;
        private readonly CartContext _context;

        public CartController(IAspNetUser aspNetUser, CartContext context)
        {
            _aspNetUser = aspNetUser;
            _context = context;
        }

        [HttpGet("cart")]
        public async Task<CustomerCart> GetCart()
        {
            return await GetCustomerCart() ?? new CustomerCart();
        }

        [HttpPost("cart")]
        public async Task<IActionResult> AddProductCart(ProductCart product)
        {
            var cart = await GetCustomerCart();

            if (cart == null)
            {
                HandleNewCart(product);
            }
            else
            {
                HandleExistingCart(cart, product);
            }

            ValidateCart(cart);
            if (!ValidOperation()) return CustomResponse();

            await SaveChanges();
            return CustomResponse();
        }

        private void HandleNewCart(ProductCart product)
        {
            var cart = new CustomerCart(_aspNetUser.GetUserId());

            cart.AddProduct(product);

            _context.CustomerCart.Add(cart);
        }

        private void HandleExistingCart(CustomerCart cart, ProductCart product) 
        {
            var existingProduct = cart.ExistingProductCart(product);

            cart.AddProduct(product);

            if (existingProduct)
            {
                _context.ProductsCart.Update(cart.GetProductById(product.Id));
            }
            else
            {
                _context.ProductsCart.Add(product);
            }

            _context.CustomerCart.Update(cart);
        }

        [HttpGet("cart/{productId}")]
        public async Task<IActionResult> UpdateProductCart(Guid productId, ProductCart productCart)
        {
            var cart = await GetCustomerCart();
            var validatedProductCart = await GetValidatedProductCart(productId, cart, productCart);

            if (validatedProductCart == null)
            {
                return CustomResponse();
            }

            cart.UpdateAmount(validatedProductCart, productCart.ProductAmount);

            ValidateCart(cart);
            if (!ValidOperation()) return CustomResponse();

            _context.ProductsCart.Update(validatedProductCart);
            _context.CustomerCart.Update(cart);
            await SaveChanges();

            return CustomResponse();
        }

        private async Task SaveChanges()
        {
            var result = await _context.SaveChangesAsync();
            if (result <= 0)
            {
                AddProccessError("Not able to save the changes on database");
            }
        }

        [HttpDelete("cart/{productId}")]
        public async Task<IActionResult> DeleteProductCart(Guid productId)
        {
            var cart = await GetCustomerCart();

            var validatedProductCart = await GetValidatedProductCart(productId, cart);
            if (validatedProductCart == null)
            {
                return CustomResponse();
            }

            ValidateCart(cart);
            if (!ValidOperation()) return CustomResponse();

            cart.DeleteProduct(validatedProductCart);

            _context.ProductsCart.Remove(validatedProductCart);
            _context.CustomerCart.Update(cart);
            await SaveChanges();

            return CustomResponse();
        }

        private async Task<CustomerCart> GetCustomerCart()
        {
            return await _context.CustomerCart
                            .Include(c => c.Products)
                            .FirstOrDefaultAsync(c => c.CustomerId == _aspNetUser.GetUserId());
        }

        private async Task<ProductCart?> GetValidatedProductCart(Guid productId, CustomerCart cart, ProductCart product = null)
        {
            if (product != null && productId != product.ProductId)
            {
                AddProccessError("The product information doesn't match");
                return null;
            }

            if (cart == null) 
            {
                AddProccessError("Cart not found");
                return null;
            }

            var productCart = await _context.ProductsCart
                .FirstOrDefaultAsync(i => i.CartId == cart.Id && i.ProductId == productId);

            if (productCart == null || !cart.ExistingProductCart(productCart))
            {
                AddProccessError("The product wasn't found in the cart");
                return null;
            }

            return productCart;
        }

        private bool ValidateCart(CustomerCart cart)
        {
            if (cart.IsValid()) return true;

            cart.ValidationResult.Errors.ToList()
                .ForEach(error => AddProccessError(error.ErrorMessage));

            return false;
        }
    }
}
