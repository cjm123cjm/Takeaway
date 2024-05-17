using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace Takeaway.Services.ProductAPI.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductId { get; set; } = null!;

        [BsonElement("Name")]
        [BsonRequired]
        public string Name { get; set; } = null!;

        [BsonRequired]
        public string Description { get; set; } = null!;

        public string CategoryName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
