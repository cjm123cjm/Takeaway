using System;
using Takeaway.Web.Models;
using Takeaway.Web.Service.IService;
using Takeaway.Web.Utility;

namespace Takeaway.Web.Service
{
	public class CouponService : ICouponService
	{
		private readonly IBaseService _baseService;

		public CouponService(IBaseService baseService)
		{
			_baseService = baseService ?? throw new ArgumentNullException(nameof(baseService));
		}
		public async Task<ResponseDto?> GetCouponByCodeAsync(string couponCode)
		{
			return await _baseService.SendAsync(new RequestDto
			{
				ApiType = SD.ApiType.GET,
				Url = SD.CouponAPIBase + "/api/coupon/GetCouponByCode/" + couponCode
			});
		}
		public async Task<ResponseDto?> GetCouponsAsync()
		{
			return await _baseService.SendAsync(new RequestDto
			{
				ApiType = SD.ApiType.GET,
				Url = SD.CouponAPIBase + "/api/coupon"
			});
		}
		public async Task<ResponseDto?> GetCouponByIdAsync(int couponId)
		{
			return await _baseService.SendAsync(new RequestDto
			{
				ApiType = SD.ApiType.GET,
				Url = SD.CouponAPIBase + "/api/coupon/" + couponId
			});
		}

		public async Task<ResponseDto?> CreateCouponAsync(CouponDto couponDto)
		{
			return await _baseService.SendAsync(new RequestDto
			{
				ApiType = SD.ApiType.POST,
				Url = SD.CouponAPIBase + "/api/coupon/",
				Data = couponDto
			});
		}
		public async Task<ResponseDto?> UpdateCouponAsync(CouponDto couponDto)
		{
			return await _baseService.SendAsync(new RequestDto
			{
				ApiType = SD.ApiType.PUT,
				Url = SD.CouponAPIBase + "/api/coupon/",
				Data = couponDto
			});
		}

		public async Task<ResponseDto?> DeleteCouponAsync(int id)
		{
			return await _baseService.SendAsync(new RequestDto
			{
				ApiType = SD.ApiType.DELETE,
				Url = SD.CouponAPIBase + "/api/coupon/" + id,
			});
		}
	}
}
