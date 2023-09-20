namespace Domains.Entities
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = default!;
        public double Weight { get; set; } = default!;
        public double UnitPrice { get; set; } = default!;
        public int UnitsInStock { get; set; } = 0;
        public Guid CategoryId { get; set; } = default!;
        public Category Category { get; set; } = default!;

        public ICollection<OrderDetail> OrderDetails { get; set; } = default!;
    }
}
