using AutoMapper;
using Takeaway.Services.ProductAPI.Models;
using Takeaway.Services.ProductAPI.Models.Dtos;
using Takeaway.Services.ProductAPI.Protos;

namespace Takeaway.Services.ProductAPI.Mapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ProductDto, ProductProtoDto>().ReverseMap();
        }
    }
}
