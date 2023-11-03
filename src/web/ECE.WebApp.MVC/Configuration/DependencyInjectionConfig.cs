using ECE.WebApp.MVC.Extensions;
using ECE.WebApp.MVC.Services;

namespace ECE.WebApp.MVC.Configuration
{
	public static class DependencyInjectionConfig
	{
		public static void RegisterServices(this IServiceCollection services)
		{
			services.AddHttpClient<IAuthenticationService, AuthenticationService>();

			services.AddHttpClient<ICatalogService, CatalogService>();

			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			services.AddScoped<IUser, AspNetUser>();
		}
	}
}
