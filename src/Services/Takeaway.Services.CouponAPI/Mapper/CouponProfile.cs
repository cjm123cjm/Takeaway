using AutoMapper;
using Takeaway.Services.CouponAPI.Models;
using Takeaway.Services.CouponAPI.Models.Dtos;

namespace Takeaway.Services.CouponAPI.Mapper
{
	public class CouponProfile : Profile
	{
        public CouponProfile()
        {
            CreateMap<Coupon, CouponDto>();
        }
    }
}
