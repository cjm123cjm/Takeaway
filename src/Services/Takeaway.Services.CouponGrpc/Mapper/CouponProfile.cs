using AutoMapper;
using Takeaway.Services.CouponGrpc.Models;
using Takeaway.Services.CouponGrpc.Models.Dtos;
using Takeaway.Services.CouponGrpc.Protos;

namespace Takeaway.Services.CouponGrpc.Mapper
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
