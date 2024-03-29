using ECE.Catalog.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

builder.Services.AddApiConfiguration(builder.Configuration);

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseApiConfiguration(app.Environment);

app.MapControllers();

app.Run();
