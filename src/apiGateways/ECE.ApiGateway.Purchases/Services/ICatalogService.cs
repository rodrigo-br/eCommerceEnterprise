using ECE.ApiGateway.Purchases.Models;

namespace ECE.ApiGateway.Purchases.Services
{
    public interface ICatalogService
    {
        Task<ProductDTO> GetProductById(Guid productId);
    }
}
