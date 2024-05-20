using AutoMapper;
using Takeaway.Services.ShoppingCartAPI.Models;
using Takeaway.Services.ShoppingCartAPI.Models.Dtos;

namespace Takeaway.Services.ShoppingCartAPI.Mapper
{
    public class ShoppingCartProfile : Profile
    {
        public ShoppingCartProfile() 
        {
            CreateMap<CardDetails, CardDetailsDto>().ReverseMap();
            CreateMap<CardHeader, CardHeaderDto>().ReverseMap();
        }
    }
}
