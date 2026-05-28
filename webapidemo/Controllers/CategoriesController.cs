using demowebapi.Models;
using Microsoft.AspNetCore.Mvc;
using webapidemo.DTOs;
using demowebapi.Services;

namespace demowebapi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET ALL CATEGORIES
        [HttpGet]
        public ActionResult<IEnumerable<CategoryDTO>> GetCategories()
        {
            var categories = _categoryService.GetAll()
                .Select(c => new CategoryDTO { CatId = c.CatId, CategoryName = c.CategoryName });

            return Ok(categories);
        }

        // UPDATE CATEGORY
        [HttpPut("{cid}")]
        public ActionResult<CategoryDTO> UpdateCategory(int cid, CategoryUpdateDTO category)
        {
            var updated = _categoryService.Update(cid, new Category { CategoryName = category.CategoryName });

            if (updated == null)
            {
                return NotFound(new { Message = "Category Not Found" });
            }

            var cDTO = new CategoryDTO
            {
                CatId = updated.CatId,
                CategoryName = updated.CategoryName
            };

            return Ok(cDTO);
        }

        // DELETE CATEGORY
        [HttpDelete("{cid}")]
        public ActionResult DeleteCategory(int cid)
        {
            var deleted = _categoryService.Delete(cid);

            if (!deleted)
            {
                return NotFound(new { Message = "Category Not Found" });
            }

            return NoContent();
        }

        // GET CATEGORY BY ID
        [HttpGet("{cid}")]
        public ActionResult<CategoryDTO> GetCategoryById(int cid)
        {
            var category = _categoryService.GetById(cid);

            if (category == null)
            {
                return NotFound(new { Message = "Category Not Found" });
            }

            return Ok(new CategoryDTO { CatId = category.CatId, CategoryName = category.CategoryName });
        }

        // ADD CATEGORY
        [HttpPost]
        public ActionResult<CategoryDTO> AddCategory(CategoryCreateDTO category)
        {
            var newCategory = new Category { CategoryName = category.CategoryName };
            var added = _categoryService.Add(newCategory);

            var cDTO = new CategoryDTO
            {
                CatId = added.CatId,
                CategoryName = added.CategoryName
            };

            return CreatedAtAction(nameof(GetCategoryById), new { cid = cDTO.CatId }, cDTO);
        }
    }
}
