using Domains.Entities;

namespace eStoreClient.Models.ProductDTO
{
    public class ProductDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = default!;
        public double Weight { get; set; } = default!;
        public double UnitPrice { get; set; } = default!;
        public int UnitsInStock { get; set; } = 0;

        public Category Category { get; set; } = default!;
    }
}
