using Microsoft.OpenApi.Models;

namespace ECE.Catalog.API.Configuration
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(config =>
            config.SwaggerDoc(name: "v1", new OpenApiInfo
            {
                Title = "eCommerce Enterprise Catalog API",
                Description = "API for handling catalog service in eCommerce Enterprise.",
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
