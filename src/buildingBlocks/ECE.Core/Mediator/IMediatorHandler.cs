using ECE.Core.Messages;
using FluentValidation.Results;

namespace ECE.Core.Mediator
{
	public interface IMediatorHandler
	{
		Task PublishEvent<T>(T _event) where T : Event;

		Task<ValidationResult> SendCommand<T>(T command) where T : Command;
	}
}
