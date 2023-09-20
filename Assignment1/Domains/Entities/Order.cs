namespace Domains.Entities
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public DateTime RequiredDate { get; set; } = DateTime.UtcNow;
        public DateTime ShippedDate { get; set; } = DateTime.UtcNow.AddDays(3);
        public double Freight { get; set; } = default!;
        public Guid MemberId { get; set; } = default!;
        public Member Member { get; set; } = default!;
        public ICollection<OrderDetail> OrderDetails { get; set; } = default!;
    }
}
