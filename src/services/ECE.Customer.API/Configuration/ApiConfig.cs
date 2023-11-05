using ECE.Customer.API.Data;
using Microsoft.EntityFrameworkCore;
using ECE.WebApi.Core.Identity;

namespace ECE.Customer.API.Configuration
{
	public static class ApiConfig
	{
		public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<CustomerContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

			services.AddControllers();

			services.AddJwtConfiguration(configuration);

			services.AddCors(options =>
			{
				options.AddPolicy("Total", builder =>
					builder.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader());
			});
			services.AddEndpointsApiExplorer();
			services.AddSwaggerConfiguration();
		}

		public static void UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseCors("Total");
			app.UseSwaggerConfiguration(env);
			app.UseJwtConfiguration();
		}
	}
}
