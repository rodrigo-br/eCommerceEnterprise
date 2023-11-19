using ECE.ApiGateway.Purchases.Extensions;
using Microsoft.Extensions.Options;

namespace ECE.ApiGateway.Purchases.Services
{
    public class PaymentService : Service, IPaymentService
    {
        private readonly HttpClient _httpClient;

        public PaymentService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.CartUrl);
        }
    }
}
