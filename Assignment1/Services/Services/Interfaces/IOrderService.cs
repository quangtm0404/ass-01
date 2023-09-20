using Services.ViewModels.OrderDTO;

namespace Services.Services.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<OrderViewDTO> GetAllOrders();
        OrderViewDTO GetOrderById(Guid id);
        OrderViewDTO CreateOrder(OrderCreateDTO order);
        OrderViewDTO UpdateOrder(OrderUpdateDTO order);
        bool DeleteOrder(Guid id);
        IEnumerable<OrderViewDTO> GetByDates(DateTime startTime, DateTime endTime);
        IEnumerable<OrderViewDTO> GetByUser(Guid userId);
    }
}
