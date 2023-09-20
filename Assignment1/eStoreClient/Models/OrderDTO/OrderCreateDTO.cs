
using eStoreClients.Models.OrderDetailDTO;

namespace eStoreClient.Models.OrderDTO
{
    public class OrderCreateDTO
    {
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public DateTime RequiredDate { get; set; } = DateTime.UtcNow;
        public DateTime ShippedDate { get; set; } = DateTime.UtcNow.AddDays(3);
        public double Freight { get; set; } = default!;
        public Guid MemberId { get; set; } = default!;
        public ICollection<OrderDetailCreateDTO> OrderDetails { get; set; } = default!;
    }
}
