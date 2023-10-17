using AutoMapper;
using Domains.Entities;
using Services.Repositories;
using Services.Services.Interfaces;
using Services.ViewModels.OrderDTO;

namespace Services.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IMapper _mapper;
        public OrderService(IMapper mapper, IOrderRepository orderRepository, IProductRepository productRepository, IOrderDetailRepository orderDetailRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
        }
        public OrderViewDTO CreateOrder(OrderCreateDTO orderDTO)
        {
            var order = _mapper.Map<Order>(orderDTO);
            var orderDetail = order.OrderDetails;
            foreach (var orderItem in orderDetail)
            {
                orderItem.OrderId = order.Id;
            }
            order.OrderDetails = null;
            _orderRepository.Insert(order);
            // Check order Details is valid or not 
            _orderRepository.SaveChanges();




            foreach (var item in orderDetail)
            {
                var prod = _productRepository.Find(x => x.Id == item.ProductId, x => x.Category).FirstOrDefault();
                if (prod is not null)
                {
                    if (prod.UnitsInStock < item.Quantity)
                    {
                        throw new Exception("One or Many product(s) is not enough quantity");
                    }
                    else
                    {
                        prod.UnitsInStock -= item.Quantity;
                        _orderDetailRepository.Insert(item);
                        _orderDetailRepository.SaveChanges();
                        _productRepository.Update(prod);

                        _productRepository.SaveChanges();
                    }
                }
                else
                {
                    throw new Exception("One or Many Product(s) is not valid!");
                }
            }


            return _mapper.Map<OrderViewDTO>(_orderRepository.Find(x => x.Id == order.Id).First());

        }

        public bool DeleteOrder(Guid id)
        {
            var order = _orderRepository.Find(x => x.Id == id, x => x.OrderDetails).First();
            if (order.OrderDetails is not null)
            {
                if (order.OrderDetails.Count > 0)
                {
                    throw new Exception("Had OrderDetails");
                }
                throw new Exception("Had Order Details");
            }
            else
            {
                _orderRepository.Delete(order);
                return _orderRepository.SaveChanges();
            }
        }

        public IEnumerable<OrderViewDTO> GetAllOrders()
            => _mapper.Map<IEnumerable<OrderViewDTO>>(_orderRepository.GetAll(x => x.OrderDetails));
        public IEnumerable<OrderViewDTO> GetByDates(DateTime startTime, DateTime endTime)
        {
            if (startTime.Date >= endTime.Date) throw new Exception("Start Time is larger or equal than End Time");
            return _mapper.Map<IEnumerable<OrderViewDTO>>(_orderRepository.Find(x => x.OrderDate >= startTime && x.OrderDate <= endTime, x => x.OrderDetails));
        }

        public IEnumerable<OrderViewDTO> GetByUser(Guid userId)
        => _mapper.Map<IEnumerable<OrderViewDTO>>(_orderRepository.Find(x => x.MemberId == userId, x => x.OrderDetails));



        public OrderViewDTO GetOrderById(Guid id)
        => _mapper.Map<OrderViewDTO>(_orderRepository.Find(x => x.Id == id, x => x.OrderDetails).First());



        public OrderViewDTO UpdateOrder(OrderUpdateDTO order)
        {
            _orderRepository.Update(_mapper.Map<Order>(order));
            return _orderRepository.SaveChanges()
                ? _mapper.Map<OrderViewDTO>(_orderRepository.Find(x => x.Id == order.Id, x => x.OrderDetails))
                : throw new Exception("Save changes failed!");
        }
    }
}
