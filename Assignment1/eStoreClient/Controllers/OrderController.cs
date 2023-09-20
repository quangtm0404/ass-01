using eStoreClient.Models.OrderDTO;
using eStoreClient.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eStoreClient.Controllers
{
	public class OrderController : Controller
	{
		private readonly IOrderService _orderService;
		private readonly IMemberService _memberService;
		public OrderController(IOrderService orderService, IMemberService memberService)
		{
			_orderService = orderService;
			_memberService = memberService;
		}
		public async Task<IActionResult> Index()
		{
			var result = await _orderService.GetAllOrders();
			if (result is not null && result.IsSuccess)
			{
				var orderList = JsonConvert.DeserializeObject<IEnumerable<OrderViewDTO>>(Convert.ToString(result.Result)!);
				return View(orderList);
			}
			else
			{
				TempData["error"] = "Not have any orders!";
				return RedirectToAction("Index", "Home");
			}
		}

		[HttpGet]
		public async Task<IActionResult> Details(Guid id)
		{
			var result = await _orderService.GetOrderById(id);
			if (result is not null && result.IsSuccess)
			{
				var order = JsonConvert.DeserializeObject<OrderViewDTO>(Convert.ToString(result.Result)!);
				return View(order);
			}
			else
			{
				TempData["error"] = "Not found!";
				return RedirectToAction(nameof(Index));
			}
		}

		[HttpGet]
		public async Task<IActionResult> OrderHistory(Guid id)
		{

			var result = await _orderService.GetOrderByUser(id);
			if (result is not null && result.IsSuccess)
			{
				var orderList = JsonConvert.DeserializeObject<IEnumerable<OrderViewDTO>>(Convert.ToString(result.Result)!);
				return View(nameof(Index), orderList);
			}
			else
			{
				TempData["error"] = "You have not ordered anything yet!";
				return RedirectToAction("Index", "Home");
			}
		}

		[HttpPost]
		public async Task<IActionResult> Filter(DateTime startDate, DateTime endDate)
		{
			var result = await _orderService.GetOrderByDate(startDate, endDate);
			if (result is not null && result.IsSuccess)
			{
				var orderList = JsonConvert.DeserializeObject<IEnumerable<OrderViewDTO>>(Convert.ToString(result.Result)!);
				return View(nameof(Index), orderList);
			}
			else
			{
				TempData["error"] = "Not found any Orders in the range of date you input";
				return RedirectToAction(nameof(Index));
			}

		}




	}
}
