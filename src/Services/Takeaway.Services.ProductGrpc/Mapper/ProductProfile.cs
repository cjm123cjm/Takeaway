using AutoMapper;
using Takeaway.Services.ProductGrpc.Models;
using Takeaway.Services.ProductGrpc.Models.Dtos;
using Takeaway.Services.ProductGrpc.Protos;

namespace Takeaway.Services.ProductGrpc.Mapper
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
