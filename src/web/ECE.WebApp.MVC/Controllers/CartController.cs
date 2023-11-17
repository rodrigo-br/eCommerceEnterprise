using ECE.WebApp.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECE.WebApp.MVC.Controllers
{
    public class CartController : MainController
    {
        [HttpGet]
        [Route("cart")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        [Route("cart/add-product")]
        public async Task<IActionResult> AddProductCart(ProductCartViewModel product)
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("cart/update-product")]
        public async Task<IActionResult> UpdateProductCart(Guid productId, int amount)
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("cart/delete-product")]
        public async Task<IActionResult> DeleteProductCart(Guid productId)
        {
            return RedirectToAction("Index");
        }
    }
}
