using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Takeaway.Services.ProductGrpc.Config;
using Takeaway.Services.ProductGrpc.Models;

namespace Takeaway.Services.ProductGrpc.Data
{
    public class ProductContext : IProductContext
    {
        public ProductContext(IOptions<ProductDatabase> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            var database = client.GetDatabase(options.Value.DatabaseName);
            Products = database.GetCollection<Product>(options.Value.ProductCollectionName);

            //添加种子数据
            ProductContextSeed.SeedData(Products);
        }
        public IMongoCollection<Product> Products { get; }
    }
}
