namespace Services.ViewModels.OrderDTO
{
    public class OrderUpdateDTO : OrderCreateDTO
    {
        public Guid Id { get; set; } = default!;
    }
}
