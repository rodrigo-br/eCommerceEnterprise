using ECE.ApiGateway.Purchases.Models;
using ECE.Core.Communication;

namespace ECE.ApiGateway.Purchases.Services
{
    public interface ICartService
    {
        Task<CartDTO> GetCart();
        Task<int> GetCartAmount();
        Task<ResponseResult> AddProductCart(ProductCartDTO productCart);
        Task<ResponseResult> UpdateProductCart(ProductCartDTO productCart);
        Task<ResponseResult> DeleteProductCart(Guid productId);
    }
}
