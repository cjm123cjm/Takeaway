using AutoMapper;
using Takeaway.Services.ProductAPI.Models;
using Takeaway.Services.ProductAPI.Models.Dtos;

namespace Takeaway.Services.ProductAPI.Mapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
