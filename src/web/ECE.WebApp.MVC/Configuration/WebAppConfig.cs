namespace ECE.WebApp.MVC.Configuration
{
    public static class WebAppConfig
    {
        public static void AddWebAppConfiguration(this IServiceCollection services)
        {
            services.AddControllersWithViews();
        }

        public static void UseWebAppConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseIdentityConfiguration();
        }
    }
}
