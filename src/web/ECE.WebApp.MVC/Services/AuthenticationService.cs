using ECE.WebApp.MVC.Extensions;
using ECE.WebApp.MVC.Models;
using Microsoft.Extensions.Options;

namespace ECE.WebApp.MVC.Services
{
	public class AuthenticationService : Service, IAuthenticationService
	{
		private readonly HttpClient _httpClient;

		public AuthenticationService(HttpClient httpClient,
									IOptions<AppSettings> appSettings)
		{
			_httpClient = httpClient;
			_httpClient.BaseAddress = new Uri(appSettings.Value.AuthenticationUrl
				?? throw new ArgumentNullException(nameof(appSettings.Value.AuthenticationUrl)));
		}

		public async Task<UserResponseLogin> Login(UserLogin userLogin)
		{
			var loginContent = SerializeToStringContent(userLogin);

			var response = await _httpClient.PostAsync("/api/auth/login", loginContent);

			if (!HandleResponseErrors(response))
			{
				return new UserResponseLogin
				{
					ResponseResult = await DeserializeObjectResponse<ResponseResult>(response)
				};
			}

			return await DeserializeObjectResponse<UserResponseLogin>(response);
		}

		public async Task<UserResponseLogin> Register(UserRegister userRegister)
		{
			var registerContent = SerializeToStringContent(userRegister);

			var response = await _httpClient.PostAsync("/api/auth/new-account", registerContent);

			if (!HandleResponseErrors(response))
			{
				return new UserResponseLogin
				{
					ResponseResult = await DeserializeObjectResponse<ResponseResult>(response)
				};
			}

			return await DeserializeObjectResponse<UserResponseLogin>(response);
		}
	}
}