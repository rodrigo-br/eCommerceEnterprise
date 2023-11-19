using ECE.WebApi.Core.User;
using ECE.WebApp.MVC.Extensions;
using ECE.WebApp.MVC.Services;
using ECE.WebApp.MVC.Services.Handlers;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Polly;
using ECE.WebApi.Core.Extensions;

namespace ECE.WebApp.MVC.Configuration
{
	public static class DependencyInjectionConfig
	{
		public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton<IValidationAttributeAdapterProvider, CpfValidationAttributeAdapterProvider>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddScoped<IAspNetUser, AspNetUser>();


			services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

			services.AddHttpClient<IAuthenticationService, AuthenticationService>()
                .WaitAndRetry();

            services.AddHttpClient<ICatalogService, CatalogService>()
				.AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                .WaitAndRetry();

            services.AddHttpClient<ICartService, CartService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                .WaitAndRetry();
            //services.AddHttpClient("Refit", options =>
            //	{
            //		options.BaseAddress = new Uri(configuration.GetSection("CatalogUrl").Value);
            //	}).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
            //	.AddTypedClient(Refit.RestService.For<ICatalogServiceRefit>);

        }
	}
}
