using ECE.Catalog.API.Data.Repository;
using ECE.Catalog.API.Data;
using ECE.Catalog.API.Models;

namespace ECE.Catalog.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<CatalogContext>();
        }
    }
}
