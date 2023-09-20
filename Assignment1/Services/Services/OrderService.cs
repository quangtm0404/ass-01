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
		private readonly IMapper _mapper;
		public OrderService(IMapper mapper, IOrderRepository orderRepository, IProductRepository productRepository)
		{
			_mapper = mapper;
			_productRepository = productRepository;
			_orderRepository = orderRepository;
		}
		public OrderViewDTO CreateOrder(OrderCreateDTO orderDTO)
		{
			var order = _mapper.Map<Order>(orderDTO);
			_orderRepository.Insert(order);
			// Check order Details is valid or not 
			var orderDetails = order.OrderDetails;
			if (orderDetails != null && orderDetails.Count > 0)
			{
				foreach (var item in orderDetails)
				{
					var prod = _productRepository.Find(x => x.Id == item.ProductId).FirstOrDefault();
					if (prod is not null)
					{
						if (prod.UnitsInStock < item.Quantity)
						{
							throw new Exception("One or Many product(s) is not enought quantity");
						}
						else
						{
							prod.UnitsInStock -= item.Quantity;
							_productRepository.Update(prod);
						}
					}
					else
					{
						throw new Exception("One or Many Product(s) is not valid!");
					}
				}
			}
			if (_orderRepository.SaveChanges())
				return _mapper.Map<OrderViewDTO>(_orderRepository.Find(x => x.Id == order.Id).First());
			else throw new Exception("Save changes failed!");
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
