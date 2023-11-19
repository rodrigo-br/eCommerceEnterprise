using ECE.Core.Mediator;
using ECE.Order.Domain.Vouchers;
using ECE.Order.Infra.Data;
using ECE.Order.Infra.Data.Repository;

namespace ECE.Order.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IVoucherRepository, VoucherRepository>();
            services.AddScoped<OrderContext>();
        }
    }
}
