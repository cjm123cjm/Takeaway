using AutoMapper;
using Grpc.Core;
using Takeaway.Services.ProductAPI.Protos;
using Takeaway.Services.ProductAPI.Repositories;

namespace Takeaway.Services.ProductAPI.Services
{
    public class ProductService : ProductProtoService.ProductProtoServiceBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductService> _logger;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, ILogger<ProductService> logger, IMapper mapper)
        {
            _productRepository = productRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public override async Task<ProductResponse> GetProducts(ProductRequest request, ServerCallContext context)
        {
            ProductResponse productResponse = new ProductResponse();
            try
            {
                var products = await _productRepository.GetProductsAsync(request.ProductIds.ToList());

                _logger.LogInformation("Product is retrieved for ProductIds:{productIds}", request.ProductIds.ToList());

                productResponse.IsSuccess = true;

                var proto = _mapper.Map<List<ProductProtoDto>>(products);

                productResponse.ProductDto.AddRange(proto);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Product is retrieved for ProductIds:{productIds}", request.ProductIds.ToList());
                productResponse.IsSuccess = false;
                productResponse.Message = "读取失败";
            }

            return productResponse;
        }
    }
}
