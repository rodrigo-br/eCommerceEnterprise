using ECE.ApiGateway.Purchases.Models;
using ECE.ApiGateway.Purchases.Services;
using ECE.WebApi.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECE.ApiGateway.Purchases.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class CartController : MainController
    {
        private readonly ICartService _cartService;
        private readonly ICatalogService _catalogService;

        public CartController(ICartService cartService, ICatalogService catalogService)
        {
            _cartService = cartService;
            _catalogService = catalogService;
        }

        [HttpGet]
        [Route("purchases/cart")]
        public async Task<IActionResult> Index()
        {
            return CustomResponse(await _cartService.GetCart());
        }

        [HttpGet]
        [Route("purchases/cart-amount")]
        public async Task<int> GetCartAmount()
        {
            return await _cartService.GetCartAmount();
        }

        [HttpPost]
        [Route("purchases/cart/add-products")]
        public async Task<IActionResult> AddProductCart(ProductCartDTO productCart)
        {
            var product = await _catalogService.GetProductById(productCart.ProductId);
            if (!ValidOperation())
            {
                return CustomResponse();
            }

            productCart.ProductName = product.Name;
            productCart.ProductValue = product.Value;
            productCart.Image = product.Image;

            var response = await _cartService.AddProductCart(productCart);

            return CustomResponse(response);
        }

        [HttpPut]
        [Route("purchases/cart/products/{productId}")]
        public async Task<IActionResult> UpdateProductCart(Guid productId, ProductCartDTO productCart)
        {
            var product = await _catalogService.GetProductById(productId);

            await ValidateProductCart(product, productCart.ProductAmount);
            if (!ValidOperation())
            {
                return CustomResponse();
            }

            var response = await _cartService.UpdateProductCart(productId, productCart);
            return CustomResponse();
        }

        [HttpDelete]
        [Route("purchases/cart/products/{productId}")]
        public async Task<IActionResult> DeleteProductCart(Guid productId)
        {
            var product = await _catalogService.GetProductById(productId);

            if (product is null)
            {
                AddProccessError("Product doesn't exist");
                return CustomResponse();
            }
            var response = await _cartService.DeleteProductCart(productId);

            return CustomResponse(response);
        }

        private async Task ValidateProductCart(ProductDTO product, int amount)
        {
            if (product is null)
            {
                AddProccessError("Product doesn't exist");
            }
            if (amount <= 0)
            {
                AddProccessError($"You must choose at least 1 {product.Name}");
            }

            var cart = await _cartService.GetCart();
            var productCart = cart.Products.FirstOrDefault(p => p.ProductId == product.Id);

            if (productCart is not null && productCart.ProductAmount + amount > product.StockAmount)
            {
                AddProccessError($"Only {product.StockAmount} {product.Name} avaiable, but you selected {amount + productCart.ProductAmount}");
                return;
            }
            else if (amount > product.StockAmount)
            {
                AddProccessError($"Only {product.StockAmount} {product.Name} avaiable, but you selected {amount}");
            }
        }
    }
}
