using System.ComponentModel.DataAnnotations;

namespace demowebapi.Models
{
    public class Category
    {
        [Key]
        public int CatId { get; set; }

        public string CategoryName { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
