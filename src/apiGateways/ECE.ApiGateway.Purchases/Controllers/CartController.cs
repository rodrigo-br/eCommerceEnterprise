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
        [HttpGet]
        [Route("purchases/cart")]
        public async Task<IActionResult> Index()
        {
            return CustomResponse();
        }

        [HttpGet]
        [Route("purchases/cart-amount")]
        public async Task<IActionResult> GetCartAmount()
        {
            return CustomResponse();
        }

        [HttpPost]
        [Route("purchases/cart/add-products")]
        public async Task<IActionResult> AddProductCart()
        {
            return CustomResponse();
        }

        [HttpPut]
        [Route("purchases/cart/products/{productId}")]
        public async Task<IActionResult> UpdateProductCart()
        {
            return CustomResponse();
        }

        [HttpDelete]
        [Route("purchases/cart/products/{productId}")]
        public async Task<IActionResult> DeleteProductCart()
        {
            return CustomResponse();
        }
    }
}
