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

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _db.Products
                .Include(p => p.Category)
                .ToListAsync();
        }


        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _db.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task<Product> AddAsync(Product product)
        {
            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();

            // Reload with Category
            return await _db.Products
                .Include(p => p.Category)
                .FirstAsync(p => p.ProductId == product.ProductId);
        }

        public async Task<Product?> UpdateAsync(int id, Product product)
        {
            var existing = await _db.Products.FindAsync(id);

            if (existing == null)
                return null;

            existing.ProductName = product.ProductName;
            existing.ProductDescription = product.ProductDescription;
            existing.ProductPrice = product.ProductPrice;
            existing.IsAvailable = product.IsAvailable;
            existing.CatId = product.CatId;

            await _db.SaveChangesAsync();

            // Reload updated product with Category
            return await _db.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _db.Products.FindAsync(id);

            if (existing == null)
                return false;

            _db.Products.Remove(existing);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Product>> GetByPriceAvailAsync(decimal price, bool avail)
        {
            return await _db.Products
                .Include(p => p.Category)
                .Where(p => p.ProductPrice >= price && p.IsAvailable == avail)
                .ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetByPriceAvailCategoryAsync(decimal price, bool avail, int catId)
        {
            return await _db.Products
                .Include(p => p.Category)
                .Where(p =>
                    p.ProductPrice >= price &&
                    p.IsAvailable == avail &&
                    p.CatId == catId)
                .ToListAsync();
        }

    }
}
