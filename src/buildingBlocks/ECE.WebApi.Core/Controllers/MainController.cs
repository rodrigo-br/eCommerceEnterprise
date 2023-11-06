using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.Results;

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
	}
}
