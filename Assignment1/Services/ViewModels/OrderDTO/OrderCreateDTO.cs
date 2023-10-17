using Services.ViewModels.OrderDetailDTO;

namespace Services.ViewModels.OrderDTO
{
    public class OrderCreateDTO
    {
        public double Freight { get; set; } = default!;
        public Guid MemberId { get; set; } = default!;
        public double Total { get; set; } = default!;
        public ICollection<OrderDetailCreateDTO> OrderDetails { get; set; } = default!;
    }
}
