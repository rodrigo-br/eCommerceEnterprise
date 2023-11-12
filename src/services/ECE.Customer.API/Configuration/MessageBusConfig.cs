using ECE.Core.Utils;
using ECE.Customer.API.Services;
using ECE.MessageBus;

namespace ECE.Customer.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
                .AddHostedService<RegisterCustomerIntegrationHandler>();
        }
    }
}
