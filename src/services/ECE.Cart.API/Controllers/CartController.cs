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

            if (!ValidOperation()) return CustomResponse();

            var result = await _context.SaveChangesAsync();
            if (result <= 0) AddProccessError("Not able to save the changes on database");

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
            return CustomResponse();
        }

        [HttpDelete("cart/{productId}")]
        public async Task<IActionResult> DeleteProductCart(Guid productId)
        {
            return CustomResponse();
        }

        private async Task<CustomerCart> GetCustomerCart()
        {
            return await _context.CustomerCart
                            .Include(c => c.Products)
                            .FirstOrDefaultAsync(c => c.CustomerId == _aspNetUser.GetUserId());
        }
    }
}
