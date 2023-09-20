using eStoreClient.Models.OrderDTO;
using eStoreClient.ViewModels;

namespace eStoreClient.Services.Interfaces
{
    public interface IOrderService
    {
        Task<ResponseDTO?> GetOrderById(Guid id);
        Task<ResponseDTO?> CreateOrder(OrderCreateDTO model);
        Task<ResponseDTO?> GetOrderByUser(Guid userId);
        Task<ResponseDTO?> GetOrderByDate(DateTime starteDate, DateTime endDate);
        Task<ResponseDTO?> GetAllOrders();
    }
}
