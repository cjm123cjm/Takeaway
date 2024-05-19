using MongoDB.Driver;
using Takeaway.Services.ProductAPI.Models;

namespace Takeaway.Services.ProductAPI.Data
{
    public class ProductContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            var any = productCollection.Find(t => true).Any();
            if (!any)
            {
                productCollection.InsertMany(GetPreconfiguredProducts());
            }
        }
        private static IEnumerable<Product> GetPreconfiguredProducts()
        {
            return new List<Product>()
            {
                new Product
                {
                    Name = "Samosa",
                    Price = 15,
                    Description = "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
                    ImageUrl = "https://dotnetmastery.blob.core.windows.net/mango/14.jpg",
                    CategoryName = "Appetizer"
                },
                new Product
                {
                    Name = "Paneer Tikka",
                    Price = 13.99m,
                    Description = "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
                    ImageUrl = "https://dotnetmastery.blob.core.windows.net/mango/12.jpg",
                    CategoryName = "Appetizer"
                },
                new Product
                {
                    Name = "Sweet Pie",
                    Price = 10.99m,
                    Description = "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
                    ImageUrl = "https://dotnetmastery.blob.core.windows.net/mango/11.jpg",
                    CategoryName = "Dessert"
                },
                new Product
                {
                    Name = "Pav Bhaji",
                    Price = 15,
                    Description = "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
                    ImageUrl = "https://dotnetmastery.blob.core.windows.net/mango/13.jpg",
                    CategoryName = "Entree"
                }
            };
        }
    }
}
