using ECE.WebApp.MVC.Extensions;
using ECE.WebApp.MVC.Models;
using Microsoft.Extensions.Options;

namespace ECE.WebApp.MVC.Services
{
    public class CatalogService : Service, ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.CatalogUrl
                ?? throw new ArgumentNullException(nameof(settings.Value.CatalogUrl)));
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("/api/catalog/products");

            HandleResponseErrors(response);

            return await DeserializeObjectResponse<IEnumerable<ProductViewModel>>(response);
        }

        public async Task<ProductViewModel> GetByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/catalog/products/{id}");

            HandleResponseErrors(response);

            return await DeserializeObjectResponse<ProductViewModel>(response);
        }
    }
}
