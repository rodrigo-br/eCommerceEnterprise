using ECE.ApiGateway.Purchases.Extensions;
using Microsoft.Extensions.Options;

namespace ECE.ApiGateway.Purchases.Services
{
    public class CartService : Service, ICartService
    {
        private readonly HttpClient _httpClient;

        public CartService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.CartUrl);
        }
    }
}
