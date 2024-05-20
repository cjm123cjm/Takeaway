namespace Takeaway.Services.ShoppingCartAPI.Models.Dtos
{
    public class CardHeaderDto
    {
        public int UserId { get; set; }
        public string CouponCode { get; set; } = string.Empty;

        public CardHeaderDto(int userId)
        {
            UserId = userId;
        }

        /// <summary>
        /// 折扣金额
        /// </summary>
        public double DisCount { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        public double CartTotal { get; set; }

        /// <summary>
        /// 购物车详情
        /// </summary>
        public List<CardDetailsDto> CardDetails { get; set; } = new List<CardDetailsDto>();
    }
}
