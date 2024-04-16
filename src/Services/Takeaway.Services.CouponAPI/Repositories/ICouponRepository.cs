using Takeaway.Services.CouponAPI.Models.Dtos;

namespace Takeaway.Services.CouponAPI.Repositories
{
	public interface ICouponRepository
	{
		Task<IEnumerable<CouponDto>> GetCouponsAsync();
		Task<CouponDto?> GetCouponAsync(int id);
		Task<CouponDto?> GetCouponByCodeAsync(string code);
		Task<bool> AddCouponAsync(CouponDto coupon);
		Task<bool> UpdateCouponAsync(CouponDto coupon);
		Task<bool> DeleteCouponAsync(int id);
	}
}
