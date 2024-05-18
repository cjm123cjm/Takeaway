namespace Takeaway.Services.ShoppingCartAPI.Models.Dtos
{
    public class CardHeaderDto
    {
        public int UserId { get; set; }
        public string CouponCode { get; set; }
        public double DisCount { get; set; }
        public double CartTotal { get; set; }
    }
}
