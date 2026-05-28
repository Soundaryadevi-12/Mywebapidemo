using demowebapi.Models;
using Mywebapidemo.Data;
using Microsoft.EntityFrameworkCore;

namespace demowebapi.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _db;

        public ProductService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Product>> GetAllAsync() => await _db.Products.ToListAsync();

        public async Task<Product?> GetByIdAsync(int id) => await _db.Products.FirstOrDefaultAsync(p => p.ProductId == id);

        public async Task<Product> AddAsync(Product product)
        {
            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> UpdateAsync(int id, Product product)
        {
            var existing = await GetByIdAsync(id);
            if (existing == null) return null;
            existing.ProductName = product.ProductName;
            existing.ProductDescription = product.ProductDescription;
            existing.ProductPrice = product.ProductPrice;
            existing.IsAvailable = product.IsAvailable;
            existing.CatId = product.CatId;
            await _db.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await GetByIdAsync(id);
            if (existing == null) return false;
            _db.Products.Remove(existing);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Product>> GetByPriceAvailAsync(decimal price, bool avail) => await _db.Products.Where(p => p.ProductPrice >= price && p.IsAvailable == avail).ToListAsync();

        public async Task<IEnumerable<Product>> GetByPriceAvailCategoryAsync(decimal price, bool avail, int catId) => await _db.Products.Where(p => p.ProductPrice >= price && p.IsAvailable == avail && p.CatId == catId).ToListAsync();
    }
}
