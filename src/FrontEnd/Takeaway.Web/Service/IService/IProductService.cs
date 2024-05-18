using Takeaway.Web.Models;

namespace Takeaway.Web.Service.IService
{
    public interface IProductService
    {
        /// <summary>
        /// 获取所有产品
        /// </summary>
        /// <returns></returns>
        Task<ResponseDto?> GetProductsAsync();

        /// <summary>
        /// 根据Id获取产品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResponseDto?> GetProductAsync(string id);

        /// <summary>
        /// 根据名称获取产品
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<ResponseDto?> GetProductByNameAsync(string name);

        /// <summary>
        /// 根据类别获取产品
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        Task<ResponseDto?> GetProductByCategoryAsync(string categoryName);

        /// <summary>
        /// 创建产品
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Task<ResponseDto?> CreateProductAsync(ProductDto product);

        /// <summary>
        /// 更新产品
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Task<ResponseDto?> UpdateProductAsync(ProductDto product);

        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResponseDto?> DeleteProductAsync(string id);
    }
}
