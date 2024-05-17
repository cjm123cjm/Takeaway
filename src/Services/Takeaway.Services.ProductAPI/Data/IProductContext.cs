using MongoDB.Driver;
using Takeaway.Services.ProductAPI.Models;

namespace Takeaway.Services.ProductAPI.Data
{
    public interface IProductContext
    {
        IMongoCollection<Product> Products { get; }
    }
}
