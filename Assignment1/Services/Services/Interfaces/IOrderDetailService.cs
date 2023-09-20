using Domains.Entities;

namespace Services.Services.Interfaces
{
    public interface IOrderDetailService
    {
        OrderDetail CreateOrderDetail(OrderDetail orderDetail);
        IEnumerable<OrderDetail> GetAllOrders();
        OrderDetail GetOrderDetailById(Guid id);
        bool DeleteOrderDetail(OrderDetail orderDetail);

    }
}
