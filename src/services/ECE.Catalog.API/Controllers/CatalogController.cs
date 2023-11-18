using ECE.Catalog.API.Models;
using ECE.WebApi.Core.Controllers;
using ECE.WebApi.Core.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECE.Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CatalogController : MainController
    {
        private readonly IProductRepository _productRepository;

        public CatalogController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [AllowAnonymous]
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
