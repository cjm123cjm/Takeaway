using MongoDB.Driver;
using Takeaway.Services.ProductGrpc.Models;

namespace Takeaway.Services.ProductGrpc.Data
{
    public interface IProductContext
    {
        IMongoCollection<Product> Products { get; }
    }
}
