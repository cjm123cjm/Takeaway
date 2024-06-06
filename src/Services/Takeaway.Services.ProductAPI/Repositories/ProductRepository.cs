using AutoMapper;
using MongoDB.Driver;
using System.Xml.Linq;
using Takeaway.Services.ProductAPI.Data;
using Takeaway.Services.ProductAPI.Models;
using Takeaway.Services.ProductAPI.Models.Dtos;

namespace Takeaway.Services.ProductAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IProductContext _productContext;
        private readonly IMapper _mapper;
        public ProductRepository(IProductContext productContext, IMapper mapper)
        {
            _productContext = productContext;
            _mapper = mapper;
        }

        /// <summary>
        /// 获取所有产品
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var products = await _productContext.Products.Find(t => true).ToListAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        /// <summary>
        /// 根据Id获取产品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ProductDto> GetProductAsync(string id)
        {
            var product = await _productContext.Products.Find(t => t.ProductId == id).FirstOrDefaultAsync();
            return _mapper.Map<ProductDto>(product);
        }

        /// <summary>
        /// 根据多个Id获取产品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ProductDto>> GetProductsAsync(List<string> ids)
        {
            var products = await _productContext.Products.Find(t => ids.Contains(t.ProductId)).ToListAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        /// <summary>
        /// 根据名称获取产品
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ProductDto>> GetProductByNameAsync(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.StringIn(t => t.Name, name);

            var products = await _productContext.Products.Find(filter).ToListAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        /// <summary>
        /// 根据类别获取产品
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ProductDto>> GetProductByCategoryAsync(string categoryName)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(t => t.CategoryName, categoryName);

            var products = await _productContext.Products.Find(filter).ToListAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        /// <summary>
        /// 创建产品
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task CreateProductAsync(ProductDto product)
        {
            var model = _mapper.Map<Product>(product);
            await _productContext.Products.InsertOneAsync(model);
        }

        /// <summary>
        /// 更新产品
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<bool> UpdateProductAsync(ProductDto product)
        {
            var model = _mapper.Map<Product>(product);

            var updateResult = await _productContext.Products.ReplaceOneAsync(filter: t => t.ProductId == model.ProductId, replacement: model);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteProductAsync(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(t => t.ProductId, id);
            var deleteResult = await _productContext.Products.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
    }
}
