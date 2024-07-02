using Takeaway.Services.ProductGrpc.Models.Dtos;

namespace Takeaway.Services.ProductGrpc.Repositories
{
    public interface IProductRepository
    {
        /// <summary>
        /// 获取所有产品
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ProductDto>> GetProductsAsync();

        /// <summary>
        /// 根据Id获取产品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ProductDto> GetProductAsync(string id);

        /// <summary>
        /// 根据多个Id获取产品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IEnumerable<ProductDto>> GetProductsAsync(List<string> ids);

        /// <summary>
        /// 根据名称获取产品
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<IEnumerable<ProductDto>> GetProductByNameAsync(string name);

        /// <summary>
        /// 根据类别获取产品
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        Task<IEnumerable<ProductDto>> GetProductByCategoryAsync(string categoryName);

        /// <summary>
        /// 创建产品
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Task CreateProductAsync(ProductDto product);

        /// <summary>
        /// 更新产品
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Task<bool> UpdateProductAsync(ProductDto product);

        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteProductAsync(string id);
    }
}
