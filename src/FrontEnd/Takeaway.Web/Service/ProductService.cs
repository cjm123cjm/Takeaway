using Takeaway.Web.Models;
using Takeaway.Web.Service.IService;
using Takeaway.Web.Utility;

namespace Takeaway.Web.Service
{
    public class ProductService : IProductService
    {
        private readonly IBaseService _baseService;
        public ProductService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        /// <summary>
        /// 获取所有产品
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseDto?> GetProductsAsync()
        {
            return await _baseService.SendAsync(new RequestDto
            {
                Url = SD.ProductAPIBase + "/api/product",
                ApiType = SD.ApiType.GET
            }, withBearer: false);
        }

        /// <summary>
        /// 根据Id获取产品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResponseDto?> GetProductAsync(string id)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                Url = SD.ProductAPIBase + "/api/product/" + id,
                ApiType = SD.ApiType.GET
            }, withBearer: false);
        }

        /// <summary>
        /// 根据名称获取产品
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<ResponseDto?> GetProductByNameAsync(string name)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                Url = SD.ProductAPIBase + "/api/product/GetProductByName/" + name,
                ApiType = SD.ApiType.GET
            }, withBearer: false);
        }

        /// <summary>
        /// 根据类别获取产品
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public async Task<ResponseDto?> GetProductByCategoryAsync(string categoryName)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                Url = SD.ProductAPIBase + "/api/product/GetProductByCategory/" + categoryName,
                ApiType = SD.ApiType.GET
            }, withBearer: false);
        }

        /// <summary>
        /// 创建产品
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<ResponseDto?> CreateProductAsync(ProductDto product)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                Url = SD.ProductAPIBase + "/api/product",
                ApiType = SD.ApiType.POST,
                Data = product
            });
        }

        /// <summary>
        /// 更新产品
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<ResponseDto?> UpdateProductAsync(ProductDto product)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                Url = SD.ProductAPIBase + "/api/product",
                ApiType = SD.ApiType.PUT,
                Data = product
            });
        }

        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResponseDto?> DeleteProductAsync(string id)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                Url = SD.ProductAPIBase + "/api/product/" + id,
                ApiType = SD.ApiType.DELETE
            });
        }
    }
}
