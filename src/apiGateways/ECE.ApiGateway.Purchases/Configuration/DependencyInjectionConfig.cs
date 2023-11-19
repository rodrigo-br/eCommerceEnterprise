using ECE.ApiGateway.Purchases.Extensions;
using ECE.ApiGateway.Purchases.Services;
using ECE.WebApi.Core.User;
using ECE.WebApi.Core.Extensions;
using Polly;

namespace ECE.ApiGateway.Purchases.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();
            services.AddTransient<HttpClientAuthorizationDelegationHandler>();

            services.AddHttpClient<ICatalogService, CatalogService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegationHandler>()
                .WaitAndRetry();

            services.AddHttpClient<ICartService, CartService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegationHandler>()
                .WaitAndRetry();
        }
    }
}
