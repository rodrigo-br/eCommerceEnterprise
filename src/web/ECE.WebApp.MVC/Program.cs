using ECE.WebApp.MVC.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
	.AddJsonFile("appsettings.json", true, true)
	.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
	.AddEnvironmentVariables();

builder.Services.AddWebAppConfiguration(builder.Configuration);
builder.Services.AddIdentityConfiguration();
builder.Services.RegisterServices();

var app = builder.Build();

app.UseWebAppConfiguration(app.Environment);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
