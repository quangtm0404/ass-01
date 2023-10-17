namespace Domains.Entities
{
    public class OrderDetail
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public double UnitPrice { get; set; } = default!;
        public int Quantity { get; set; } = default!;
        public double Discount { get; set; } = default!;
        public string ProductName { get; set; } = default!;
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = default!;
        public Guid OrderId { get; set; } = default!;
        public Order Order { get; set; } = default!;
    }
}
