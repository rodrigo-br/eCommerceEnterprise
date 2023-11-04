using Polly.CircuitBreaker;
using Refit;
using System.Net;

namespace ECE.WebApp.MVC.Extensions
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _requestDelegate;

		public ExceptionMiddleware(RequestDelegate requestDelegate)
		{
			_requestDelegate = requestDelegate;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _requestDelegate(httpContext);
			}
            catch (Exception ex)
			when (ex is CustomHttpResponseException ||
					ex is ValidationApiException || 
					ex is ApiException)
            {
                HandleResponseExceptionAsync(httpContext, ((dynamic)ex).StatusCode);
            }
			catch(BrokenCircuitException ex)
			{
				HandleCircuitBreakerExceptionAsync(httpContext);
			}
        }

		private static void HandleResponseExceptionAsync(HttpContext httpContext, HttpStatusCode statusCode)
		{
			if (statusCode == HttpStatusCode.Unauthorized)
			{
				httpContext.Response.Redirect($"/login?ReturnUrl={httpContext.Request.Path}");
				return;
			}

			httpContext.Response.StatusCode = (int)statusCode;
		}

		private static void HandleCircuitBreakerExceptionAsync(HttpContext context)
		{
			context.Response.Redirect("/system-out");
		}
	}
}
