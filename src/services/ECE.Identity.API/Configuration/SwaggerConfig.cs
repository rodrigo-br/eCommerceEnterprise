using Microsoft.OpenApi.Models;

namespace ECE.Identity.API.Configuration
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(config =>
            config.SwaggerDoc(name: "v1", new OpenApiInfo
            {
                Title = "eCommerce Enterprise Identity API",
                Description = "API for handling user authentication and authorization in eCommerce Enterprise.",
                Contact = new OpenApiContact
                {
                    Name = "Rodrigo Alves",
                    Email = "rodrigoab123@gmail.com"
                },
                License = new OpenApiLicense
                {
                    Name = "MIT",
                    Url = new Uri("https://opensource.org/licenses/MIT")
                }
            }));

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(config =>
                {
                    config.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "v1");
                });
            }

            return app;
        }
    }
}
