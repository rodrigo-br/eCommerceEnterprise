using ECE.Core.Communication;
using ECE.WebApp.MVC.Extensions;
using ECE.WebApp.MVC.Models;
using Microsoft.Extensions.Options;

namespace ECE.WebApp.MVC.Services
{
    public class CartService : Service, ICartService
    {
        private readonly HttpClient _httpClient;

        public CartService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.CartUrl
                ?? throw new ArgumentNullException(nameof(settings.Value.CartUrl)));
        }

        public async Task<CustomerCartViewModel> GetCart()
        {
            var response = await _httpClient.GetAsync("/api/cart");

            HandleResponseErrors(response);

            return await DeserializeObjectResponse<CustomerCartViewModel>(response);
        }

        public async Task<ResponseResult> AddProductCart(ProductCartViewModel product)
        {
            var productContent = SerializeToStringContent(product);

            var response = await _httpClient.PostAsync("/api/cart/", productContent);

            if (!HandleResponseErrors(response))
            {
                return await DeserializeObjectResponse<ResponseResult>(response);
            }

            return new ResponseResult();
        }

        public async Task<ResponseResult> UpdateProductCart(Guid productId, ProductCartViewModel product)
        {
            var productContent = SerializeToStringContent(product);

            var response = await _httpClient.PutAsync($"/api/cart/{product.ProductId}", productContent);

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
    }
}
