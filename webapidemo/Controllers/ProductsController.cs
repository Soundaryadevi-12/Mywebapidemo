using demowebapi.Models;
using Microsoft.AspNetCore.Mvc;
using webapidemo.DTOs;
using demowebapi.Services;

namespace demowebapi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // UPDATE PRODUCT
        [HttpPut("{pid}")]
        public async Task<ActionResult<ProductDTO>> UpdateProduct(int pid, ProductUpdateDTO product)
        {
            var existing = await _productService.GetByIdAsync(pid);

            if (existing == null)
            {
                return NotFound(new { Message = "Product Not Found" });
            }

            var updated = await _productService.UpdateAsync(pid, new Product
            {
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                ProductPrice = product.ProductPrice,
                IsAvailable = product.IsAvailable,
                CatId = product.CatId
            });

            var pDTO = new ProductDTO
            {
                ProductId = updated!.ProductId,
                ProductName = updated.ProductName,
                ProductPrice = updated.ProductPrice,
                CatId = updated.CatId,
                IsAvailable = updated.IsAvailable,
                ProductDescription = updated.ProductDescription
            };

            return Ok(pDTO);
        }

        // DELETE PRODUCT
        [HttpDelete("{pid}")]
        public async Task<ActionResult> DeleteProduct(int pid)
        {
            var deleted = await _productService.DeleteAsync(pid);

            if (!deleted)
            {
                return NotFound(new { Message = "Product Not Found" });
            }

            return NoContent();
        }
        [HttpGet]
        // GET ALL PRODUCTS
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            var products = (await _productService.GetAllAsync())
                .Select(p => new ProductDTO
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    ProductDescription = p.ProductDescription,
                    ProductPrice = p.ProductPrice,
                    IsAvailable = p.IsAvailable,
                    CatId = p.CatId
                });

            return Ok(products);
        }

        // GET PRODUCT BY ID
        [HttpGet("{pid}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int pid)
        {
            var product = await _productService.GetByIdAsync(pid);

            if (product == null)
            {
                return NotFound(new { Message = "Product Not Found" });
            }

            var dto = new ProductDTO
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                ProductPrice = product.ProductPrice,
                IsAvailable = product.IsAvailable,
                CatId = product.CatId
            };

            return Ok(dto);
        }

        ////Multiple Route Parameters
        ////2 parameters: price and availability
        //[Route("api/[controller]")]
        //[HttpGet("Price/{pPrice}/Avail/{pAvail}")]
        //public ActionResult<IEnumerable<Product>> GetProdPriceAvail(decimal pPrice, bool pAvail)
        //{
        //    var filterProd = _products
        //        .Where(p => p.ProductPrice >= pPrice && p.IsAvailable == pAvail)
        //        .ToList();

        //    if (!filterProd.Any())
        //        return NotFound();

        //    return Ok(filterProd);
        //}

        ////3 parameters: price, availability and category id which returns product object
        //[Route("api/[controller]")]
        //[HttpGet("Price/{ProductPrice}/Avail/{IsAvailable}/CategoryId/{CatId}")]
        //public ActionResult<IEnumerable<Product>> GetProdPriceAvailCatId(decimal ProductPrice, bool IsAvailable, int CatId)
        //{
        //    var filterProd = _products
        //        .Where(p => p.ProductPrice >= ProductPrice && p.IsAvailable == IsAvailable && p.CatId == CatId)
        //        .ToList();

        //    if (!filterProd.Any())
        //        return NotFound();

        //    return Ok(filterProd);
        //}
        ////3 parameters: price, availability and category id which returns productDTO
        //[Route("api/[controller]")]
        //[HttpGet("Price/{ProductPrice}/Avail/{IsAvailable}/CategoryId/{CatId}")]
        //public ActionResult<IEnumerable<ProductDTO>> GetProdPriceAvailCatId(decimal ProductPrice, bool IsAvailable, int CatId)
        //{
        //    var filterProd = _products
        //        .Where(p => p.ProductPrice >= ProductPrice && p.IsAvailable == IsAvailable && p.CatId == CatId)
        //        .Select(p => new ProductDTO
        //        {
        //            ProductId = p.ProductId,
        //            ProductName = p.ProductName,
        //            ProductDescription = p.ProductDescription,
        //            ProductPrice = p.ProductPrice,
        //            IsAvailable = p.IsAvailable,
        //            CatId = p.CatId
        //        })
        //        .ToList();

        //    if (!filterProd.Any())
        //        return NotFound();

        //    return Ok(filterProd);
        //}

        // ADD PRODUCT
        [HttpPost]
        public async Task<ActionResult<ProductDTO>> AddProduct(ProductCreateDTO product)
        {
            var newproduct = new Product
            {
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                CatId = product.CatId,
                IsAvailable = product.IsAvailable,
                ProductDescription = product.ProductDescription
            };

            var added = await _productService.AddAsync(newproduct);

            var pDTO = new ProductDTO
            {
                ProductId = added.ProductId,
                ProductName = added.ProductName,
                ProductPrice = added.ProductPrice,
                CatId = added.CatId,
                IsAvailable = added.IsAvailable,
                ProductDescription = added.ProductDescription
            };

            return CreatedAtAction(nameof(GetProductById), new { pid = pDTO.ProductId }, pDTO);
        }
    }
}
