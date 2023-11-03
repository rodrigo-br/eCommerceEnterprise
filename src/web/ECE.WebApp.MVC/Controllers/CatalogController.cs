using ECE.WebApp.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECE.WebApp.MVC.Controllers
{
    public class CatalogController : MainController
    {
        private readonly ICatalogService _catalogService;

        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [HttpGet]
        [Route("")]
        [Route("showcase")]
        public async Task<IActionResult> Index()
        {
            var products = await _catalogService.GetAllAsync();
            return View(products);
        }

        [HttpGet]
        [Route("product-detail/{id}")]
        public async Task<IActionResult> ProductDetail(Guid id)
        {
            var product = await _catalogService.GetByIdAsync(id);
            return View(product);
        }

    }
}
