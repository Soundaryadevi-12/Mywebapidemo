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
        public ActionResult<ProductDTO> UpdateProduct(int pid, ProductUpdateDTO product)
        {
            var existing = _productService.GetById(pid);

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
            var existing = _productService.GetById(pid);

            if (existing == null)
            {
                return NotFound(new { Message = "Product Not Found" });
            }

            _productService.Delete(pid);

            return NoContent();
        }
        [HttpGet]
        // GET ALL PRODUCTS
        public ActionResult<IEnumerable<ProductDTO>> GetProducts()
        {
            var products = _productService.GetAll()
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
        public ActionResult<Product> GetProductById(int pid)
        {
            var product = _productService.GetById(pid);

            if (product == null)
            {
                return NotFound(new
                {
                    Message = "Product Not Found"
                });
            }

            return Ok(product);
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
        public ActionResult<ProductDTO> AddProduct(ProductCreateDTO product)
        {
            var newproduct = new Product
            {
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                CatId = product.CatId,
                IsAvailable = product.IsAvailable,
                ProductDescription = product.ProductDescription
            };

            var added = _productService.Add(newproduct);

            var pDTO = new ProductDTO
            {
                ProductId = added.ProductId,
                ProductName = added.ProductName,
                ProductPrice = added.ProductPrice,
                CatId = added.CatId,
                IsAvailable = added.IsAvailable,
                ProductDescription = added.ProductDescription
            };

            return CreatedAtAction(nameof(GetProductById),
                new { pid = pDTO.ProductId }, pDTO);
        }
    }
}
