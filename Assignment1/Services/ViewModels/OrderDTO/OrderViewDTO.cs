using Services.ViewModels.OrderDetailDTO;

namespace Services.ViewModels.OrderDTO
{
    public class OrderViewDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public DateTime RequiredDate { get; set; } = DateTime.UtcNow;
        public DateTime ShippedDate { get; set; } = DateTime.UtcNow.AddDays(3);
        public double Freight { get; set; } = default!;
        public double Total { get; set; } = default!;
        public Guid MemberId { get; set; } = default!;
        public ICollection<OrderDetailViewDTO> OrderDetails { get; set; } = default!;
    }
}
