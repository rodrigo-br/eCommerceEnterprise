using ECE.ApiGateway.Purchases.Extensions;
using ECE.ApiGateway.Purchases.Models;
using Microsoft.Extensions.Options;

namespace ECE.ApiGateway.Purchases.Services
{
    public class CatalogService : Service, ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.CartUrl);
        }

        public async Task<ProductDTO> GetProductById(Guid productId)
        {
            var response = await _httpClient.GetAsync($"/api/catalog/products/{productId}");

            HandleResponseErrors(response);

            return await DeserializeObjectResponse<ProductDTO>(response);
        }
    }
}
