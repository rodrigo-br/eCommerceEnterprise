using ECE.WebApp.MVC.Models;
using Refit;

namespace ECE.WebApp.MVC.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<ProductViewModel>> GetAllAsync();
        Task<ProductViewModel> GetByIdAsync(Guid id);
    }

    public interface ICatalogServiceRefit
    {
        [Get("/api/catalog/products")]
        Task<IEnumerable<ProductViewModel>> GetAllAsync();

        [Get("/api/catalog/products/{id}")]
        Task<ProductViewModel> GetByIdAsync(Guid id);
    }
}
