using demowebapi.Models;
using Microsoft.AspNetCore.Mvc;
using webapidemo.DTOs;

namespace demowebapi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly List<Category> _categories = new List<Category>
        {
            new Category { CatId = 1, CategoryName = "Electronics" },
            new Category { CatId = 2, CategoryName = "Accessories" },
            new Category { CatId = 3, CategoryName = "Fashion" }
        };

        // GET ALL CATEGORIES
        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetCategories()
        {
            return Ok(_categories);
        }

        // UPDATE CATEGORY
        [HttpPut("{cid}")]
        public ActionResult<CategoryDTO> UpdateCategory(int cid, CategoryUpdateDTO category)
        {
            var existing = _categories.FirstOrDefault(c => c.CatId == cid);

            if (existing == null)
            {
                return NotFound(new { Message = "Category Not Found" });
            }

            existing.CategoryName = category.CategoryName;

            var cDTO = new CategoryDTO
            {
                CatId = existing.CatId,
                CategoryName = existing.CategoryName
            };

            return Ok(cDTO);
        }

        // DELETE CATEGORY
        [HttpDelete("{cid}")]
        public ActionResult DeleteCategory(int cid)
        {
            var existing = _categories.FirstOrDefault(c => c.CatId == cid);

            if (existing == null)
            {
                return NotFound(new { Message = "Category Not Found" });
            }

            _categories.Remove(existing);

            return NoContent();
        }

        // GET CATEGORY BY ID
        [HttpGet("{cid}")]
        public ActionResult<Category> GetCategoryById(int cid)
        {
            var category = _categories.FirstOrDefault(c => c.CatId == cid);

            if (category == null)
            {
                return NotFound(new
                {
                    Message = "Category Not Found"
                });
            }
            return Ok(category);
        }

        // ADD CATEGORY
        [HttpPost]
        public ActionResult<CategoryDTO> AddCategory(CategoryCreateDTO category)
        {
            var newCategory = new Category
            {
                CatId = _categories.Max(c => c.CatId + 1),
                CategoryName = category.CategoryName
            };

            _categories.Add(newCategory);

            var cDTO = new CategoryDTO
            {
                CatId = newCategory.CatId,
                CategoryName = newCategory.CategoryName
            };

            return CreatedAtAction(nameof(GetCategoryById), new { cid = cDTO.CatId }, cDTO);
        }
    }
}
