using ECE.Core.Mediator;
using ECE.Customer.API.Application.Commands;
using ECE.Customer.API.Application.Events;
using ECE.Customer.API.Data;
using ECE.Customer.API.Data.Repository;
using ECE.Customer.API.Models;
using ECE.Customer.API.Services;
using FluentValidation.Results;
using MediatR;

namespace ECE.Customer.API.Configuration
{
	public static class DependencyInjectionConfig
	{
		public static void RegisterServices(this IServiceCollection services)
		{
			services.AddScoped<IMediatorHandler, MediatorHandler>();
			services.AddScoped<IRequestHandler<RegisterCustomerCommand, ValidationResult>, CustomerCommandHandler>();
			services.AddScoped<INotificationHandler<RegisteredCustomerEvent>, CustomerEventHandler>();
			services.AddScoped<ICustomerRepository, CustomerRepository>();

			services.AddScoped<CustomerContext>();

			services.AddHostedService<RegisterCustomerIntegrationHandler>();
		}
	}
}
