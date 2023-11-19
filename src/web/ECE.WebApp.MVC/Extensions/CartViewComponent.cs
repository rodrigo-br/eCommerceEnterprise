using ECE.WebApp.MVC.Services;
using Microsoft.AspNetCore.Mvc;
using ECE.WebApp.MVC.Models;

namespace ECE.WebApp.MVC.Extensions
{
    public class CartViewComponent : ViewComponent
    {
        private readonly IPurchasesGatewayService _purchaseService;

        public CartViewComponent(IPurchasesGatewayService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _purchaseService.GetCartAmount());
        }
    }
}
