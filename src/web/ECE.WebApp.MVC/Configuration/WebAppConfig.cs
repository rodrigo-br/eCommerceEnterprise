using ECE.WebApp.MVC.Extensions;

namespace ECE.WebApp.MVC.Configuration
{
    public static class WebAppConfig
    {
        public static void AddWebAppConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews();

            services.Configure<AppSettings>(configuration);
        }

        public static void UseWebAppConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (!env.IsDevelopment())
            //{
                app.UseExceptionHandler("/erro/500");
                app.UseStatusCodePagesWithRedirects("/erro/{0}");
                app.UseHsts();
            //}
            //else
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseIdentityConfiguration();
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
