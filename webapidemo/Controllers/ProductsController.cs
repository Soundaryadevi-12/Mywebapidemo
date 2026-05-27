using demowebapi.Models;
using Microsoft.AspNetCore.Mvc;
using webapidemo.DTOs;

namespace demowebapi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // Category List
        private readonly List<Category> _categories = new List<Category>
        {
            new Category { CatId = 1, CategoryName = "Electronics" },
            new Category { CatId = 2, CategoryName = "Accessories" },
            new Category { CatId = 3, CategoryName = "Fashion" }
        };

        // Product List
        private readonly List<Product> _products = new()
        {
            new Product
            {
                ProductId = 101,
                ProductName = "Laptop",
                ProductDescription = "Dell Laptop",
                ProductPrice = 90000,
                IsAvailable = true,
                CatId = 1
            },

            new Product
            {
                ProductId = 102,
                ProductName = "Mobile",
                ProductDescription = "Samsung Mobile",
                ProductPrice = 25000,
                IsAvailable = true,
                CatId = 1
            },

            new Product
            {
                ProductId = 103,
                ProductName = "Headphones",
                ProductDescription = "Boat Headphones",
                ProductPrice = 2000,
                IsAvailable = true,
                CatId = 2
            }

        };

        // UPDATE PRODUCT
        [HttpPut("{pid}")]
        public ActionResult<ProductDTO> UpdateProduct(int pid, ProductUpdateDTO product)
        {
            var existing = _products.FirstOrDefault(p => p.ProductId == pid);

            if (existing == null)
            {
                return NotFound(new { Message = "Product Not Found" });
            }

            // update fields
            existing.ProductName = product.ProductName;
            existing.ProductDescription = product.ProductDescription;
            existing.ProductPrice = product.ProductPrice;
            existing.IsAvailable = product.IsAvailable;
            existing.CatId = product.CatId;

            var pDTO = new ProductDTO
            {
                ProductId = existing.ProductId,
                ProductName = existing.ProductName,
                ProductPrice = existing.ProductPrice,
                CatId = existing.CatId,
                IsAvailable = existing.IsAvailable,
                ProductDescription = existing.ProductDescription
            };

            return Ok(pDTO);
        }

        // DELETE PRODUCT
        [HttpDelete("{pid}")]
        public ActionResult DeleteProduct(int pid)
        {
            var existing = _products.FirstOrDefault(p => p.ProductId == pid);

            if (existing == null)
            {
                return NotFound(new { Message = "Product Not Found" });
            }

            _products.Remove(existing);

            return NoContent();
        }
        [HttpGet]
        // GET ALL PRODUCTS
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            return Ok(_products);
        }

        // GET PRODUCT BY ID
        [HttpGet("{pid}")]
        public ActionResult<Product> GetProductById(int pid)
        {
            var product = _products.FirstOrDefault(p => p.ProductId == pid);

            if (product == null)
            {
                return NotFound(new
                {
                    Message = "Product Not Found"
                });
            }

            return Ok(product);
        }

        // ADD PRODUCT
        [HttpPost]
        public ActionResult<ProductDTO> AddProduct(ProductCreateDTO product)
        {
            var newproduct = new Product
            {
                ProductId = _products.Max(p => p.ProductId + 1),
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                CatId = product.CatId,
                IsAvailable = product.IsAvailable,
                ProductDescription = product.ProductDescription
            };

            _products.Add(newproduct);

            var pDTO = new ProductDTO
            {
                ProductId = newproduct.ProductId,
                ProductName = newproduct.ProductName,
                ProductPrice = newproduct.ProductPrice,
                CatId = newproduct.CatId,
                IsAvailable = newproduct.IsAvailable,
                ProductDescription = newproduct.ProductDescription
            };

            return CreatedAtAction(nameof(GetProductById),
                new { pid = pDTO.ProductId }, pDTO);
        }
    }
}
