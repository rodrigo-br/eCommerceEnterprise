using ECE.ApiGateway.Purchases.Extensions;
using Microsoft.Extensions.Options;

namespace ECE.ApiGateway.Purchases.Services
{
    public class OrdererService : Service, IOrdererService
    {
        private readonly HttpClient _httpClient;

        public OrdererService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.CartUrl);
        }
    }
}
