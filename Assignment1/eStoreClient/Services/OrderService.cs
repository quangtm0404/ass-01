using eStoreClient.Models.OrderDTO;
using eStoreClient.Services.Interfaces;
using eStoreClient.Utilities;
using eStoreClient.ViewModels;

namespace eStoreClient.Services
{
	public class OrderService : IOrderService
	{
		private readonly IBaseService _baseService;
		public OrderService(IBaseService baseService)
		{
			_baseService = baseService;
		}
		public async Task<ResponseDTO?> CreateOrder(OrderCreateDTO model)
		=> await _baseService.SendAsync(new RequestDTO
		{
			URL = $"{StaticDetail.eStoreAPIBase}/api/orders",
			Data = model,
			APIType = StaticDetail.APIType.POST

		});

		public async Task<ResponseDTO?> GetAllOrders()
			=> await _baseService.SendAsync(new RequestDTO
			{
				APIType = StaticDetail.APIType.GET,
				URL = $"{StaticDetail.eStoreAPIBase}/api/orders"
			});


		public async Task<ResponseDTO?> GetOrderByDate(DateTime starteDate, DateTime endDate)
		=> await _baseService.SendAsync(new RequestDTO
		{
			URL = $"{StaticDetail.eStoreAPIBase}/api/orders/statistic?startDate={starteDate}&endDate={endDate}",
			APIType = StaticDetail.APIType.GET

		});

		public async Task<ResponseDTO?> GetOrderById(Guid id)
		=> await _baseService.SendAsync(new RequestDTO
		{
			URL = $"{StaticDetail.eStoreAPIBase}/api/orders/{id}",
			APIType = StaticDetail.APIType.GET
		});

		public async Task<ResponseDTO?> GetOrderByUser(Guid id)
		=> await _baseService.SendAsync(new RequestDTO
		{
			APIType = StaticDetail.APIType.GET,
			URL = $"{StaticDetail.eStoreAPIBase}/api/users/orders/{id}"

		});


	}
}
