using System.ComponentModel.DataAnnotations;

namespace Takeaway.Services.ShoppingCartAPI.Models.Dtos
{
    public class ProductDto
    {
        public string ProductId { get; set; }
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
    }
}
