using ECE.ApiGateway.Purchases.Extensions;
using ECE.ApiGateway.Purchases.Models;
using ECE.Core.Communication;
using Microsoft.Extensions.Options;

namespace ECE.ApiGateway.Purchases.Services
{
    public class CartService : Service, ICartService
    {
        private readonly HttpClient _httpClient;

        public CartService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.CartUrl);
        }

        public async Task<CartDTO> GetCart()
        {
            var response = await _httpClient.GetAsync("/api/cart/");

            HandleResponseErrors(response);

            return await DeserializeObjectResponse<CartDTO>(response);
        }

        public async Task<int> GetCartAmount()
        {
            var response = await _httpClient.GetAsync("/api/cart/count");

            HandleResponseErrors(response);

            return await DeserializeObjectResponse<int>(response);
        }

        public async Task<ResponseResult> AddProductCart(ProductCartDTO productCart)
        {
            var productContent = SerializeToStringContent(productCart);

            var response = await _httpClient.PostAsync("/api/cart/", productContent);

            if (!HandleResponseErrors(response))
            {
                return await DeserializeObjectResponse<ResponseResult>(response);
            }

            return new ResponseResult();
        }

        public async Task<ResponseResult> DeleteProductCart(Guid productId)
        {
            var response = await _httpClient.DeleteAsync($"/api/cart/{productId}");

            if (!HandleResponseErrors(response))
            {
                return await DeserializeObjectResponse<ResponseResult>(response);
            }

            return new ResponseResult();
        }

        public async Task<ResponseResult> UpdateProductCart(ProductCartDTO productCart)
        {
            var productContent = SerializeToStringContent(productCart);

            var response = await _httpClient.PutAsync($"/api/cart/{productCart.ProductId}", productContent);

            if (!HandleResponseErrors(response))
            {
                return await DeserializeObjectResponse<ResponseResult>(response);
            }

            return new ResponseResult();
        }
    }
}
