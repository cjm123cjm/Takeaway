namespace Takeaway.Services.ShoppingCartAPI.Models.Dtos
{
    public class CardDetailsDto
    {
        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 产品Id
        /// </summary>
        public string ProductId { get; set; } = null!;

        /// <summary>
        /// 产品信息
        /// </summary>
        public ProductDto? ProductDto { get; set; }
    }
}
