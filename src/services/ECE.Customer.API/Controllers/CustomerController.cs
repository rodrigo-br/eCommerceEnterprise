using ECE.Core.Mediator;
using ECE.Customer.API.Application.Commands;
using ECE.WebApi.Core.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ECE.Customer.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CustomerController : MainController
	{
		private readonly IMediatorHandler _mediatorHandler;

		public CustomerController(IMediatorHandler mediatorHandler)
		{
			_mediatorHandler = mediatorHandler;
		}

		[HttpGet("customers")]
		public async Task<IActionResult> Index()
		{
			var result = await _mediatorHandler.SendCommand(new RegisterCustomerCommand(Guid.NewGuid(), "Cavalinho", "Cavalinho@Teste.com", "541.683.248-77"));

			return CustomResponse(result);
		}
	}
}
