﻿using System.Security.Claims;

namespace ECE.WebApp.MVC.Extensions
{
	public interface IUser
	{
		string Name { get; }
		Guid GetUserId();
		string GetUserEmail();
		string GetUserToken();
		bool IsAuthenticated();
		bool HasRole(string roleName);
		IEnumerable<Claim> GetClaims();
		HttpContext GetHttpContext();
	}

	public class AspNetUser : IUser
	{
		private readonly IHttpContextAccessor _contextAccessor;

		public AspNetUser(IHttpContextAccessor contextAccessor)
		{
			_contextAccessor = contextAccessor;
		}

		public string Name => _contextAccessor.HttpContext.User.Identity.Name;

		public IEnumerable<Claim> GetClaims()
		{
			return _contextAccessor.HttpContext.User.Claims;
		}

		public HttpContext GetHttpContext()
		{
			return _contextAccessor.HttpContext;
		}

		public string GetUserEmail()
		{
			return IsAuthenticated() ? _contextAccessor.HttpContext.User.GetUserEmail() : null;
		}

		public Guid GetUserId()
		{
			return IsAuthenticated() ? Guid.Parse(_contextAccessor.HttpContext.User.GetUserId()) : Guid.Empty;

		}

		public string GetUserToken()
		{
			return IsAuthenticated() ? _contextAccessor.HttpContext.User.GetUserToken() : string.Empty;
		}

		public bool HasRole(string roleName)
		{
			return _contextAccessor.HttpContext.User.IsInRole(roleName);
		}

		public bool IsAuthenticated()
		{
			return _contextAccessor.HttpContext.User.Identity.IsAuthenticated;
		}
	}

	public static class ClaimsPrincipalExtensions
	{
		public static string GetUserId(this ClaimsPrincipal principal)
		{
			if (principal == null)
			{
				throw new ArgumentNullException(nameof(principal));
			}

			var claim = principal.FindFirst("sub");
			return claim?.Value;
		}

		public static string GetUserEmail(this ClaimsPrincipal principal)
		{
			if (principal == null)
			{
				throw new ArgumentException(nameof(principal));
			}

			var claim = principal.FindFirst("email");
			return claim?.Value;
		}

		public static string GetUserToken(this ClaimsPrincipal principal)
		{
			if (principal == null)
			{
				throw new ArgumentException(nameof (principal));
			}

			var claim = principal.FindFirst("JWT");
			return claim?.Value;
		}
	}
}
