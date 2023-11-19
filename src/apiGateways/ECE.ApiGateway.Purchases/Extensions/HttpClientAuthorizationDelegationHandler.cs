using ECE.WebApi.Core.User;
using System.Net.Http.Headers;

namespace ECE.ApiGateway.Purchases.Extensions
{
    public class HttpClientAuthorizationDelegationHandler : DelegatingHandler
    {
        private readonly IAspNetUser _aspNetUser;

        public HttpClientAuthorizationDelegationHandler(IAspNetUser aspNetUser)
        {
            _aspNetUser = aspNetUser;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var authorizationHeader = _aspNetUser.GetHttpContext().Request.Headers["Authorization"];

            if (!string.IsNullOrEmpty(authorizationHeader) )
            {
                request.Headers.Add("Authorization", new List<string>() { authorizationHeader });
            }

            var token = _aspNetUser.GetUserToken();

            if (token is not null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
