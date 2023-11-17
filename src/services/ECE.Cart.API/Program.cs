using ECE.Cart.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddApiConfiguration(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseApiConfiguration(app.Environment);

app.MapControllers();

app.Run();
