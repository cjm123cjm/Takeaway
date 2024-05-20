namespace Takeaway.Services.ShoppingCartAPI.Models
{
    public class CardHeader
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 优惠卷
        /// </summary>
        public string CouponCode { get; set; } = string.Empty;

        /// <summary>
        /// 购物车详情
        /// </summary>
        public List<CardDetails> CardDetails { get; set; } = new List<CardDetails>();
    }
}
