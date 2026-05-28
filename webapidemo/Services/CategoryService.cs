using demowebapi.Models;

namespace demowebapi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly List<Category> _categories;

        public CategoryService()
        {
            _categories = new List<Category>
            {
                new Category { CatId = 1, CategoryName = "Electronics" },
                new Category { CatId = 2, CategoryName = "Accessories" },
                new Category { CatId = 3, CategoryName = "Fashion" }
            };
        }

        public IEnumerable<Category> GetAll() => _categories;

        public Category? GetById(int id) => _categories.FirstOrDefault(c => c.CatId == id);

        public Category Add(Category category)
        {
            category.CatId = _categories.Any() ? _categories.Max(c => c.CatId) + 1 : 1;
            _categories.Add(category);
            return category;
        }

        public Category? Update(int id, Category category)
        {
            var existing = GetById(id);
            if (existing == null) return null;
            existing.CategoryName = category.CategoryName;
            return existing;
        }

        public bool Delete(int id)
        {
            var existing = GetById(id);
            if (existing == null) return false;
            _categories.Remove(existing);
            return true;
        }
    }
}
