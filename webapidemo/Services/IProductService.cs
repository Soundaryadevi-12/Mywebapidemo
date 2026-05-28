using demowebapi.Models;
namespace demowebapi.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<Product> AddAsync(Product product);
        Task<Product?> UpdateAsync(int id, Product product);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Product>> GetByPriceAvailAsync(decimal price, bool avail);
        Task<IEnumerable<Product>> GetByPriceAvailCategoryAsync(decimal price, bool avail, int catId);
    }
}
