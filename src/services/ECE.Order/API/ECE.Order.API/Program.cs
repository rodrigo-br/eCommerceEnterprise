using ECE.Order.API.Configuration;
using ECE.WebApi.Core.Identity;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApiConfiguration(builder.Configuration);
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddSwaggerConfiguration();
builder.Services.AddMediatR(config =>
    config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.RegisterServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwaggerConfiguration(app.Environment);
app.UseApiConfiguration(app.Environment);

app.MapControllers();

app.Run();
