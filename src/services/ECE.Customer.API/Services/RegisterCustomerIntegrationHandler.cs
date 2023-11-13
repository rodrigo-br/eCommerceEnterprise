using EasyNetQ;
using ECE.Core.Mediator;
using ECE.Core.Messages.Integration;
using ECE.Customer.API.Application.Commands;
using ECE.MessageBus;
using FluentValidation.Results;

namespace ECE.Customer.API.Services
{
    public class RegisterCustomerIntegrationHandler : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMessageBus _bus;

        public RegisterCustomerIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }

        private void SetResponder()
        {
            _bus.RespondAsync<RegisteredCustomerIntegrationEvent, ResponseMessage>(async request =>
                            await RegisterCustomer(request));

            _bus.AdvancedBus.Connected += OnConnect;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetResponder();

            return Task.CompletedTask;
        }

        private void OnConnect(object? sender, ConnectedEventArgs e)
        {
            SetResponder();
        }

        private async Task<ResponseMessage> RegisterCustomer(RegisteredCustomerIntegrationEvent message)
        {
            var customerCommand = new RegisterCustomerCommand(message.Id, message.Name, message.Email, message.Cpf);

            ValidationResult success;

            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();

                success = await mediator.SendCommand(customerCommand);
            }

            return new ResponseMessage(success);
        }
    }
}
