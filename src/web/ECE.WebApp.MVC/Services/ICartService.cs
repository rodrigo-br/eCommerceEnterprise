using ECE.Core.Communication;
using ECE.WebApp.MVC.Models;

namespace ECE.WebApp.MVC.Services
{
    public interface ICartService
    {
        Task<CustomerCartViewModel> GetCart();
        Task<ResponseResult> AddProductCart(ProductCartViewModel product);
        Task<ResponseResult> UpdateProductCart(Guid productId, ProductCartViewModel product);
        Task<ResponseResult> DeleteProductCart(Guid productId);
    }
}
