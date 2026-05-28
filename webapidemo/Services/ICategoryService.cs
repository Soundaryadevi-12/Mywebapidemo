using demowebapi.Models;
namespace demowebapi.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAll();
        Category? GetById(int id);
        Category Add(Category category);
        Category? Update(int id, Category category);
        bool Delete(int id);
    }
}
