using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.Results;
using ECE.Core.Communication;

namespace ECE.WebApi.Core.Controllers
{
	[ApiController]
	public abstract class MainController : Controller
	{
		protected ICollection<string> Errors = new List<string>();

		protected IActionResult CustomResponse(object result = null)
		{
			if (ValidOperation())
			{
				return Ok(result);
			}

			return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
			{
				{ "Messages", Errors.ToArray() }
			}));
		}

		protected IActionResult CustomResponse(ModelStateDictionary modelState)
		{
			var errors = modelState.Values.SelectMany(e => e.Errors);
			foreach (var error in errors)
			{
				Errors.Add(error.ErrorMessage);
			}

			return CustomResponse();
		}

		protected IActionResult CustomResponse(ValidationResult validationResult)
		{
			foreach (var erro in validationResult.Errors)
			{
				AddProccessError(erro.ErrorMessage);
			}

			return CustomResponse();
		}

		protected IActionResult CustomResponse(ResponseResult response)
		{
			ResponseHasErrors(response);
			return CustomResponse();
		}

		protected bool ValidOperation()
		{
			return !Errors.Any();
		}

		protected void AddProccessError(string error)
		{
			Errors.Add(error);
		}

		protected void CleanProccessError()
		{
			Errors.Clear();
		}

		protected bool ResponseHasErrors(ResponseResult response)
		{
			if (response is null || !response.Errors.Messages.Any()) return false;

			foreach (var message in response.Errors.Messages)
			{
				AddProccessError(message);
			}
			return true;
		}
	}
}
