namespace Takeaway.Services.ProductGrpc.Config
{
    public class ProductDatabase
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string ProductCollectionName { get; set; } = null!;
    }
}
