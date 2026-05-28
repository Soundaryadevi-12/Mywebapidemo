using demowebapi.Models;
namespace demowebapi.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product? GetById(int id);
        Product Add(Product product);
        Product? Update(int id, Product product);
        bool Delete(int id);
        IEnumerable<Product> GetByPriceAvail(decimal price, bool avail);
        IEnumerable<Product> GetByPriceAvailCategory(decimal price, bool avail, int catId);
    }
}
