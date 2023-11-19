using ECE.Core.Communication;
using ECE.WebApp.MVC.Extensions;
using ECE.WebApp.MVC.Models;
using Microsoft.Extensions.Options;

namespace ECE.WebApp.MVC.Services
{
    public class PurchasesGatewayService : Service, IPurchasesGatewayService
    {
        private readonly HttpClient _httpClient;

        public PurchasesGatewayService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.PurchasesGatewayUrl
                ?? throw new ArgumentNullException(nameof(settings.Value.PurchasesGatewayUrl)));
        }

        public async Task<CustomerCartViewModel> GetCart()
        {
            var response = await _httpClient.GetAsync("/api/purchases/cart");

            HandleResponseErrors(response);

            return await DeserializeObjectResponse<CustomerCartViewModel>(response);
        }

        public async Task<int> GetCartAmount()
        {
            var response = await _httpClient.GetAsync("/api/purchases/cart-amount");

            HandleResponseErrors(response);

            return await DeserializeObjectResponse<int>(response);
        }

        public async Task<ResponseResult> AddProductCart(ProductCartViewModel product)
        {
            var productContent = SerializeToStringContent(product);

            var response = await _httpClient.PostAsync("/api/purchases/cart/add-products", productContent);

            if (!HandleResponseErrors(response))
            {
                return await DeserializeObjectResponse<ResponseResult>(response);
            }

            return new ResponseResult();
        }

        public async Task<ResponseResult> UpdateProductCart(Guid productId, ProductCartViewModel product)
        {
            var productContent = SerializeToStringContent(product);

            var response = await _httpClient.PutAsync($"/api/purchases/cart/products/{product.ProductId}", productContent);

            if (!HandleResponseErrors(response))
            {
                return await DeserializeObjectResponse<ResponseResult>(response);
            }

            return new ResponseResult();
        }

        public async Task<ResponseResult> DeleteProductCart(Guid productId)
        {
            var response = await _httpClient.DeleteAsync($"/api/purchases/cart/products/{productId}");

            if (!HandleResponseErrors(response))
            {
                return await DeserializeObjectResponse<ResponseResult>(response);
            }

            return new ResponseResult();
        }
    }
}
