using ECE.WebApp.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using ECE.WebApp.MVC.Services;

namespace ECE.WebApp.MVC.Controllers
{
    public class CartController : MainController
    {
		private readonly IPurchasesGatewayService _purchasesService;

		public CartController(IPurchasesGatewayService purchaseService)
		{
            _purchasesService = purchaseService;
		}

		[HttpGet]
        [Route("cart")]
        public async Task<IActionResult> Index()
        {
            return View(await _purchasesService.GetCart());
        }

        [HttpPost]
        [Route("cart/add-product")]
        public async Task<IActionResult> AddProductCart(ProductCartViewModel product)
        {
            var response = await _purchasesService.AddProductCart(product);

            if (ResponseHasErrors(response))
            {
                return View("Index", await _purchasesService.GetCart());
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("cart/update-product")]
        public async Task<IActionResult> UpdateProductCart(Guid productId, int amount)
        {
            var product = new ProductCartViewModel { ProductId = productId, ProductAmount = amount };
            var response = await _purchasesService.UpdateProductCart(productId, product);

            if (ResponseHasErrors(response))
            {
                return View("Index", await _purchasesService.GetCart());
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("cart/delete-product")]
        public async Task<IActionResult> DeleteProductCart(Guid productId)
        {
            var response = await _purchasesService.DeleteProductCart(productId);

            if (ResponseHasErrors(response))
            {
                return View("Index", await _purchasesService.GetCart());
            }

            return RedirectToAction("Index");
        }
    }
}
