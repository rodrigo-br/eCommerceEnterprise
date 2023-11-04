using ECE.WebApp.MVC.Extensions;
using ECE.WebApp.MVC.Services;
using ECE.WebApp.MVC.Services.Handlers;

namespace ECE.WebApp.MVC.Configuration
{
	public static class DependencyInjectionConfig
	{
		public static void RegisterServices(this IServiceCollection services)
		{
			services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

			services.AddHttpClient<IAuthenticationService, AuthenticationService>();
			services.AddHttpClient<ICatalogService, CatalogService>()
				.AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddScoped<IUser, AspNetUser>();
		}
	}
}
