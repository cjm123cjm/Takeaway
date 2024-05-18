namespace Takeaway.Services.ShoppingCartAPI.Models
{
    public class CardDetails
    {
        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 产品Id
        /// </summary>
        public string ProductId { get; set; } = null!;

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; } = null!;
    }
}
