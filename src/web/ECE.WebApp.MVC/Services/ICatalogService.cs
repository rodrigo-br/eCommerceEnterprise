using ECE.WebApp.MVC.Models;

namespace ECE.WebApp.MVC.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<ProductViewModel>> GetAllAsync();
        Task<ProductViewModel> GetByIdAsync(Guid id);
    }
}
