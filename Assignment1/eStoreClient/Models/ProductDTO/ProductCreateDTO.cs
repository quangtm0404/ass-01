namespace eStoreClient.Models.ProductDTO
{
    public class ProductCreateDTO
    {
        public string Name { get; set; } = default!;
        public double Weight { get; set; } = default!;
        public double UnitPrice { get; set; } = default!;
        public int UnitsInStock { get; set; } = 0;
        public Guid CategoryId { get; set; } = default!;
    }
}
