using System.ComponentModel.DataAnnotations;

namespace Takeaway.Web.Models
{
    public class ProductDto
    {
        public string ProductId { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; } = null!;
        [StringLength(50)]
        public string CategoryName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public decimal Price { get; set; }

        [Range(1, 100)]
        public int Count { get; set; } = 1;
    }
}
