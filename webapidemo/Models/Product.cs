using System.ComponentModel.DataAnnotations;

namespace demowebapi.Models
{
    public class Product
    {
        [Required]
        public int ProductId { get; set; } // PK

        [Required]
        public string ProductName { get; set; }

        public string ProductDescription { get; set; } = string.Empty;

        [Range(1, 100000.00)]
        public decimal ProductPrice { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        // Foreign Key
        public int CatId { get; set; }

        // Navigation Property
        public Category? Category { get; set; }
    }
}