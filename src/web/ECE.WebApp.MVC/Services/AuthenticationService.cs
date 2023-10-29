using ECE.WebApp.MVC.Models;
using System.Text;
using System.Text.Json;

namespace ECE.WebApp.MVC.Services
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly HttpClient _httpClient;

		public AuthenticationService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<UserResponseLogin> Login(UserLogin userLogin)
		{
			var loginContent = new StringContent(
				JsonSerializer.Serialize(userLogin),
				Encoding.UTF8,
				"application/json");
			var response = await _httpClient.PostAsync("https://localhost:44305/api/auth/login", loginContent);

			var options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true,
			};

			return JsonSerializer.Deserialize<UserResponseLogin>(await response.Content.ReadAsStringAsync(), options);
		}

		public async Task<UserResponseLogin> Register(UserRegister userRegister)
		{
			var registerContent = new StringContent(
				JsonSerializer.Serialize(userRegister),
				Encoding.UTF8,
				"application/json");
			var response = await _httpClient.PostAsync("https://localhost:44305/api/auth/new-account", registerContent);

			return JsonSerializer.Deserialize<UserResponseLogin>(await response.Content.ReadAsStringAsync());
		}
	}
}