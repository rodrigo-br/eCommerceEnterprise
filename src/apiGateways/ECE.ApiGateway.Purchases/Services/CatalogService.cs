using ECE.ApiGateway.Purchases.Extensions;
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
    }
}
