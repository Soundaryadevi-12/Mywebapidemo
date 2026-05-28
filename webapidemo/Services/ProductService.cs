using demowebapi.Models;
using System.Collections.Concurrent;

namespace demowebapi.Services
{
    public class ProductService : IProductService
    {
        private readonly List<Product> _products;

        public ProductService()
        {
            _products = new List<Product>
            {
                new Product { ProductId = 101, ProductName = "Laptop", ProductDescription = "Dell Laptop", ProductPrice = 90000, IsAvailable = true, CatId = 1 },
                new Product { ProductId = 102, ProductName = "Mobile", ProductDescription = "Samsung Mobile", ProductPrice = 25000, IsAvailable = true, CatId = 1 },
                new Product { ProductId = 103, ProductName = "Headphones", ProductDescription = "Boat Headphones", ProductPrice = 2000, IsAvailable = true, CatId = 2 }
            };
        }

        public IEnumerable<Product> GetAll() => _products;

        public Product? GetById(int id) => _products.FirstOrDefault(p => p.ProductId == id);

        public Product Add(Product product)
        {
            product.ProductId = _products.Any() ? _products.Max(p => p.ProductId) + 1 : 1;
            _products.Add(product);
            return product;
        }

        public Product? Update(int id, Product product)
        {
            var existing = GetById(id);
            if (existing == null) return null;
            existing.ProductName = product.ProductName;
            existing.ProductDescription = product.ProductDescription;
            existing.ProductPrice = product.ProductPrice;
            existing.IsAvailable = product.IsAvailable;
            existing.CatId = product.CatId;
            return existing;
        }

        public bool Delete(int id)
        {
            var existing = GetById(id);
            if (existing == null) return false;
            _products.Remove(existing);
            return true;
        }

        public IEnumerable<Product> GetByPriceAvail(decimal price, bool avail) => _products.Where(p => p.ProductPrice >= price && p.IsAvailable == avail);

        public IEnumerable<Product> GetByPriceAvailCategory(decimal price, bool avail, int catId) => _products.Where(p => p.ProductPrice >= price && p.IsAvailable == avail && p.CatId == catId);
    }
}
