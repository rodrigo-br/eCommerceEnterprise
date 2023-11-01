using ECE.Core.Data;

namespace ECE.Catalog.API.Models
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> FindByIdAsync(Guid id);
        void AddAsync(Product product);
        void UpdateAsync(Product product);
    }
}
