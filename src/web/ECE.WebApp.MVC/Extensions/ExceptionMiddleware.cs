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
			catch (CustomHttpResponseException ex)
			{ 
				HandleResponseExceptionAsync(httpContext, ex);
			}
		}

		private static void HandleResponseExceptionAsync(HttpContext httpContext, CustomHttpResponseException httpResponseException)
		{
			if (httpResponseException.StatusCode == HttpStatusCode.Unauthorized)
			{
				httpContext.Response.Redirect($"/login?ReturnUrl={httpContext.Request.Path}");
				return;
			}

			httpContext.Response.StatusCode = (int)httpResponseException.StatusCode;
		}
	}
}
