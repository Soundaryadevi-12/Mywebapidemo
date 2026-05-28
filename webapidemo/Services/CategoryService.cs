using demowebapi.Models;
using Mywebapidemo.Data;
using Microsoft.EntityFrameworkCore;

namespace demowebapi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _db;

        public CategoryService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Category>> GetAllAsync() => await _db.Categories.ToListAsync();

        public async Task<Category?> GetByIdAsync(int id) => await _db.Categories.FirstOrDefaultAsync(c => c.CatId == id);

        public async Task<Category> AddAsync(Category category)
        {
            await _db.Categories.AddAsync(category);
            await _db.SaveChangesAsync();
            return category;
        }

        public async Task<Category?> UpdateAsync(int id, Category category)
        {
            var existing = await GetByIdAsync(id);
            if (existing == null) return null;
            existing.CategoryName = category.CategoryName;
            await _db.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await GetByIdAsync(id);
            if (existing == null) return false;
            _db.Categories.Remove(existing);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
