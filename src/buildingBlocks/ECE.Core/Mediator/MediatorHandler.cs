using ECE.Core.Messages;
using FluentValidation.Results;
using MediatR;

namespace ECE.Core.Mediator
{
	public class MediatorHandler : IMediatorHandler
	{
		private readonly IMediator _mediator;

		public MediatorHandler(IMediator mediator)
		{
			_mediator = mediator;
		}

		public async Task PublishEvent<T>(T _event) where T : Event
		{
			await _mediator.Publish(_event);
		}

		public async Task<ValidationResult> SendCommand<T>(T command) where T : Command
		{
			return await _mediator.Send(command);
		}
	}
}
