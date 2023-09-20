

using eStoreClient.Models.OrderDTO;
using eStoreClient.Models.ProductDTO;

namespace eStoreClients.Models.OrderDetailDTO
{
    public class OrderDetailViewDTO
    {
        public Guid Id { get; set; } = default!;
        public double UnitPrice { get; set; } = default!;
        public int Quantity { get; set; } = default!;
        public double Discount { get; set; } = default!;
        public Guid OrderId { get; set; } = default!;
        public OrderViewDTO Order { get; set; } = default!;
        public Guid ProductId { get; set; } = default!;
        public ProductDTO Product { get; set; } = default!;
    }
}
