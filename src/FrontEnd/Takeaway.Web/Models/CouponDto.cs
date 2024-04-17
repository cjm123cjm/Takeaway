using System.ComponentModel.DataAnnotations;

namespace Takeaway.Web.Models
{
    public class CouponDto
    {
        public int CouponId { get; set; }
        [Required(ErrorMessage = "优惠码不能为空")]
        public string CouponCode { get; set; } = null!;
        public double DiscountAmount { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "最低数量大于0")]
        public int MinAmount { get; set; }
    }
}
