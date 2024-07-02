using AutoMapper;
using Takeaway.Services.ShoppingCartAPI.Models;
using Takeaway.Services.ShoppingCartAPI.Models.Dtos;
using Takeaway.Services.ShoppingCartAPI.Protos;

namespace Takeaway.Services.ShoppingCartAPI.Mapper
{
    public class ShoppingCartProfile : Profile
    {
        public ShoppingCartProfile() 
        {
            CreateMap<CardDetails, CardDetailsDto>().ReverseMap();
            CreateMap<CardHeader, CardHeaderDto>().ReverseMap();
            CreateMap<ProductProtoDto,ProductDto>().ReverseMap();
            CreateMap<CouponDto,CouponProtoDto>().ReverseMap();
        }
    }
}
