using System.ComponentModel.DataAnnotations;

namespace webapidemo.DTOs
{
    public class CategoryDTO
    {
        [Required]
        public int CatId { get; set; }

        [Required]
        public string CategoryName { get; set; }
    }
}
