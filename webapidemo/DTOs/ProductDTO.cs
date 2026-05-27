using System.ComponentModel.DataAnnotations;

namespace webapidemo.DTOs
{
    public class ProductDTO
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
        public int CatId { get; set; }

    }
}
