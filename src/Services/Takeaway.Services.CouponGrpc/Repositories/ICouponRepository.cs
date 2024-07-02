using Takeaway.Services.CouponGrpc.Models.Dtos;

namespace Takeaway.Services.CouponGrpc.Repositories
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
