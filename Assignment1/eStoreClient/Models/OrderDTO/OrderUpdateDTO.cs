namespace eStoreClient.Models.OrderDTO
{
    public class OrderUpdateDTO : OrderCreateDTO
    {
        public Guid Id { get; set; } = default!;
    }
}
