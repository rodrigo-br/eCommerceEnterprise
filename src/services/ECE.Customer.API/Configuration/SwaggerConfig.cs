using Microsoft.OpenApi.Models;

namespace ECE.Customer.API.Configuration
{
	public static class SwaggerConfig
	{
		public static void AddSwaggerConfiguration(this IServiceCollection services)
		{
			services.AddSwaggerGen(config =>
			{
				config.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "eCommerce Enterprise Customer API",
					Description = "API for handling customer service in eCommerce Enterprise.",
					Contact = new OpenApiContact { Name = "Rodrigo Alves", Email = "rodrigoab123@gmail.com" },
					License = new OpenApiLicense { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
				});

				config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = "Use JWT as : Bearer {token}",
					Name = "Authorization",
					Scheme = "Bearer",
					BearerFormat = "JWT",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey
				});

				config.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						new string[] {}
					}
				});
			});
		}

		public static void UseSwaggerConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI(config =>
				{
					config.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "v1");
				});
			}
		}
	}
}
