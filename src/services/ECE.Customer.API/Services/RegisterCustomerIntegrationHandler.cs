using EasyNetQ;
using ECE.Core.Mediator;
using ECE.Core.Messages.Integration;
using ECE.Customer.API.Application.Commands;
using FluentValidation.Results;

namespace ECE.Customer.API.Services
{
    public class RegisterCustomerIntegrationHandler : BackgroundService
    {
        private IBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public RegisterCustomerIntegrationHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _bus = RabbitHutch.CreateBus("host=localhost:5672");



            _bus.Rpc.RespondAsync<RegisteredCustomerIntegrationEvent, ResponseMessage>(async request =>
                new ResponseMessage(await RegisterCustomer(request)));

            return Task.CompletedTask;
        }

        private async Task<ValidationResult> RegisterCustomer(RegisteredCustomerIntegrationEvent message)
        {
            var customerCommand = new RegisterCustomerCommand(message.Id, message.Name, message.Email, message.Cpf);

            ValidationResult success;

            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();

                success = await mediator.SendCommand(customerCommand);
            }

            return success;
        }
    }
}
