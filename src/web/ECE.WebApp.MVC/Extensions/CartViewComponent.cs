using ECE.WebApp.MVC.Services;
using Microsoft.AspNetCore.Mvc;
using ECE.WebApp.MVC.Models;

namespace ECE.WebApp.MVC.Extensions
{
    public class CartViewComponent : ViewComponent
    {
        private readonly ICartService _cartService;

        public CartViewComponent(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _cartService.GetCart() ?? new CustomerCartViewModel());
        }
    }
}
