using ECE.WebApp.MVC.Extensions;

namespace ECE.WebApp.MVC.Services
{
	public abstract class Service
	{
		protected bool HandleResponseErrors(HttpResponseMessage response)
		{
			switch ((int)response.StatusCode)
			{
				case 401:
				case 403:
				case 404:
				case 500:
					throw new CustomHttpResponseException(response.StatusCode);

				case 400:
					return false;
				default:
					response.EnsureSuccessStatusCode();
					return true;
			}
		}
	}
}
