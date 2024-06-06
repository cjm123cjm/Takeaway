using AutoMapper;
using Takeaway.Services.CouponAPI.Models;
using Takeaway.Services.CouponAPI.Models.Dtos;
using Takeaway.Services.CouponAPI.Protos;

namespace Takeaway.Services.CouponAPI.Mapper
{
	public class CouponProfile : Profile
	{
        public CouponProfile()
        {
            CreateMap<Coupon, CouponDto>();
            CreateMap<CouponProtoDto,CouponDto>().ReverseMap();
        }
    }
}
