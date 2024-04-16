using Takeaway.Web.Models;

namespace Takeaway.Web.Service.IService
{
	public interface ICouponService
	{
		Task<ResponseDto?> GetCouponByCodeAsync(string couponCode);
		Task<ResponseDto?> GetCouponsAsync();
		Task<ResponseDto?> GetCouponByIdAsync(int couponId);
		Task<ResponseDto?> CreateCouponAsync(CouponDto couponDto);
		Task<ResponseDto?> UpdateCouponAsync(CouponDto couponDto);
		Task<ResponseDto?> DeleteCouponAsync(int id);
	}
}
