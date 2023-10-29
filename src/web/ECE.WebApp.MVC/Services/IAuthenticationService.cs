using ECE.WebApp.MVC.Models;

namespace ECE.WebApp.MVC.Services
{
	public interface IAuthenticationService
	{
		Task<UserResponseLogin> Login(UserLogin userLogin);

		Task<UserResponseLogin> Register(UserRegister userRegister);
	}
}
