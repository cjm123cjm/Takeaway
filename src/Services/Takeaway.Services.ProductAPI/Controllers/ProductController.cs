using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Takeaway.Services.ProductAPI.Models.Dtos;
using Takeaway.Services.ProductAPI.Repositories;

namespace Takeaway.Services.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private ResponseDto _responseDto;
        private readonly ILogger<ProductController> _logger;

        public ProductController(
            IProductRepository productRepository,
            ILogger<ProductController> logger)
        {
            _productRepository = productRepository;
            _responseDto = new ResponseDto();
            _logger = logger;
        }

        [HttpGet]
        public async Task<ResponseDto> GetProducts()
        {
            try
            {
                var productDtos = await _productRepository.GetProductsAsync();
                _responseDto.Result = productDtos;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                _logger.LogError(ex, "调用 GetResponseDto() 出错");
            }
            return _responseDto;
        }
        [HttpGet("{id}")]
        public async Task<ResponseDto> GetProduct(string id)
        {
            try
            {
                var productDto = await _productRepository.GetProductAsync(id);
                _responseDto.Result = productDto;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                _logger.LogError(ex, "调用 GetProduct() 出错", id);
            }
            return _responseDto;
        }

        [HttpGet("GetProductByName/{name}")]
        public async Task<ResponseDto> GetProductByName(string name)
        {
            try
            {
                var productDtos = await _productRepository.GetProductByNameAsync(name);
                _responseDto.Result = productDtos;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                _logger.LogError(ex, "调用 GetProductByName() 出错", name);
            }
            return _responseDto;
        }

        [HttpGet("GetProductByCategory/{categoryName}")]
        public async Task<ResponseDto> GetProductByCategory(string categoryName)
        {
            try
            {
                var productDtos = await _productRepository.GetProductByCategoryAsync(categoryName);
                _responseDto.Result = productDtos;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                _logger.LogError(ex, "调用 GetProductByCategory() 出错", categoryName);
            }
            return _responseDto;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<ResponseDto> CreateProduct(ProductDto product)
        {
            try
            {
                await _productRepository.CreateProductAsync(product);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                _logger.LogError(ex, "调用 CreateProduct() 出错", product);
            }
            return _responseDto;
        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public async Task<ResponseDto> UpdateProduct(ProductDto product)
        {
            try
            {
                var result = await _productRepository.UpdateProductAsync(product);
                _responseDto.IsSuccess = result;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                _logger.LogError(ex, "调用 UpdateProduct() 出错", product);
            }
            return _responseDto;
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ResponseDto> DeleteProduct(string id)
        {
            try
            {
                var result = await _productRepository.DeleteProductAsync(id);
                _responseDto.IsSuccess = result;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                _logger.LogError(ex, "调用 DeleteProduct() 出错", id);
            }
            return _responseDto;
        }
    }
}
