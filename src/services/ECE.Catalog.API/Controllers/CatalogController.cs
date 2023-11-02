using ECE.Catalog.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECE.Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : Controller
    {
        private readonly IProductRepository _productRepository;

        public CatalogController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("products")]
        public async Task<IEnumerable<Product>> AllProducts()
        {
            return await _productRepository.GetAllAsync();
        }

        [HttpGet("products/{id}")]
        public async Task<Product> ProductDetail(Guid id)
        {
            return await _productRepository.FindByIdAsync(id);
        }

    }
}
