using ECE.WebApp.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using ECE.WebApp.MVC.Services;

namespace ECE.WebApp.MVC.Controllers
{
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
        [Route("cart")]
        public async Task<IActionResult> Index()
        {
            return View(await _cartService.GetCart());
        }

        [HttpPost]
        [Route("cart/add-product")]
        public async Task<IActionResult> AddProductCart(ProductCartViewModel product)
        {
            var newProduct = await _catalogService.GetByIdAsync(product.ProductId);

            ValidateProduct(newProduct, product.ProductAmount);

            if (!IsValidOperation())
            {
                return View("Index", await _cartService.GetCart());
            }

            product.ProductName = newProduct!.Name;
            product.ProductValue = newProduct.Value;
            product.Image = newProduct.Image;

            var response = await _cartService.AddProductCart(product);

            if (ResponseHasErrors(response))
            {
                return View("Index", await _cartService.GetCart());
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("cart/update-product")]
        public async Task<IActionResult> UpdateProductCart(Guid productId, int amount)
        {
            var newProduct = await _catalogService.GetByIdAsync(productId);

            ValidateProduct(newProduct, amount);

            if (!IsValidOperation())
            {
                return View("Index", await _cartService.GetCart());
            }

            var product = new ProductCartViewModel { ProductId = productId, ProductAmount = amount };
            var response = await _cartService.UpdateProductCart(productId, product);

            if (ResponseHasErrors(response))
            {
                return View("Index", await _cartService.GetCart());
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("cart/delete-product")]
        public async Task<IActionResult> DeleteProductCart(Guid productId)
        {
            var product = await _catalogService.GetByIdAsync(productId);
            if (product is null)
            {
                AddValidationError("Product doesn't exist");
                return View("Index", await _cartService.GetCart());
            }

            var response = await _cartService.DeleteProductCart(productId);

            if (ResponseHasErrors(response))
            {
                return View("Index", await _cartService.GetCart());
            }

            return RedirectToAction("Index");
        }

        private void ValidateProduct(ProductViewModel product, int amount)
        {
            if (product is null)
            {
                AddValidationError("Product doesn't exist");
            }

            if (amount < 1)
            {
                AddValidationError($"Select at least 1 {product?.Name}");
            }

            if (amount > product?.StockAmount)
            {
                AddValidationError($"{product.Name} has only {product.StockAmount} pieces in stock but you selected {amount}.");
            }

        }
    }
}
