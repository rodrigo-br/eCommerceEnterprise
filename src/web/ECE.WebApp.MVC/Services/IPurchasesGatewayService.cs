using ECE.Core.Communication;
using ECE.WebApp.MVC.Models;

namespace ECE.WebApp.MVC.Services
{
    public interface IPurchasesGatewayService
    {
        Task<CustomerCartViewModel> GetCart();
        Task<int> GetCartAmount();
        Task<ResponseResult> AddProductCart(ProductCartViewModel product);
        Task<ResponseResult> UpdateProductCart(Guid productId, ProductCartViewModel product);
        Task<ResponseResult> DeleteProductCart(Guid productId);
    }
}
