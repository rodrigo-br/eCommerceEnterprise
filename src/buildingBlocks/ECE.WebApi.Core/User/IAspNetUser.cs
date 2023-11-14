using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ECE.WebApi.Core.User
{
    public interface IAspNetUser
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
}
