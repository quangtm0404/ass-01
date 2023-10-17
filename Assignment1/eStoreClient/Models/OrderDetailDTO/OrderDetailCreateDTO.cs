namespace eStoreClients.Models.OrderDetailDTO
{
    public class OrderDetailCreateDTO
    {
        public double UnitPrice { get; set; } = default!;
        public int Quantity { get; set; } = 1;
        public double Discount { get; set; } = 0;

        public string? ProductName { get; set; } = default!;
        public Guid ProductId { get; set; }
    }
}
