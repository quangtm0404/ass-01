namespace Services.ViewModels.OrderDetailDTO
{
    public class OrderDetailCreateDTO
    {
        public double UnitPrice { get; set; } = default!;
        public int Quantity { get; set; } = default!;
        public double Discount { get; set; } = default!;
        public string? ProductName { get; set; } = default!;
        public Guid ProductId { get; set; }
    }
}
