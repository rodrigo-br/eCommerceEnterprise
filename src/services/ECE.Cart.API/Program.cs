using ECE.Cart.API.Configuration;
using ECE.WebApi.Core.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApiConfiguration(builder.Configuration);
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddSwaggerConfiguration();
builder.Services.RegisterServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwaggerConfiguration(app.Environment);
app.UseApiConfiguration(app.Environment);

app.MapControllers();

app.Run();
