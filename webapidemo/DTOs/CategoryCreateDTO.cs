using System.ComponentModel.DataAnnotations;

namespace webapidemo.DTOs
{
    public class CategoryCreateDTO
    {
        [Required]
        public int CatId { get; set; }

        [Required]
        public string CategoryName { get; set; }
    }
}
