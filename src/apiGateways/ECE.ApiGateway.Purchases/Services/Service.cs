using System.Text.Json;
using System.Text;
using System.Net;

namespace ECE.ApiGateway.Purchases.Services
{
    public abstract class Service
    {
        protected StringContent SerializeToStringContent(object data)
        {
            return new StringContent(
                JsonSerializer.Serialize(data),
                Encoding.UTF8,
                "application/json");
        }

        protected async Task<T> DeserializeObjectResponse<T>(HttpResponseMessage responseMessage)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            return JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), options)
                ?? throw new ArgumentNullException(nameof(responseMessage));
        }

        protected bool HandleResponseErrors(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.BadRequest) return false;

            response.EnsureSuccessStatusCode();
            return true;
        }
    }
}
