﻿using ECE.WebApi.Core.User;
using ECE.WebApp.MVC.Extensions;
using ECE.WebApp.MVC.Services;
using ECE.WebApp.MVC.Services.Handlers;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Polly;

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
                .AddTransientHttpErrorPolicy(p =>
                    p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(600)))
                .AddTransientHttpErrorPolicy(p =>
                    p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<ICatalogService, CatalogService>()
				.AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                .AddTransientHttpErrorPolicy(p =>
                    p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(600)))
                .AddTransientHttpErrorPolicy(p =>
                    p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<ICartService, CartService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                .AddTransientHttpErrorPolicy(p =>
                    p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(600)))
                .AddTransientHttpErrorPolicy(p =>
                    p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));
            //services.AddHttpClient("Refit", options =>
            //	{
            //		options.BaseAddress = new Uri(configuration.GetSection("CatalogUrl").Value);
            //	}).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
            //	.AddTypedClient(Refit.RestService.For<ICatalogServiceRefit>);

		}
	}
}
