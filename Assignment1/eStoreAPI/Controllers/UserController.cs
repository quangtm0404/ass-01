using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Interfaces;
using Services.ViewModels;

namespace eStoreAPI.Controllers
{

	[Route("api/[controller]s")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IOrderService _orderService;
		private ResponseDTO _response;
		public UserController(IOrderService orderService)
		{
			_orderService = orderService;
			_response = new();
		}

		[Authorize(Roles = "MEMBER")]
		[HttpGet("orders/{id}")]
		public IActionResult Get(Guid id)
		{
			var result = _orderService.GetByUser(id);
			if (result.Count() > 0)
			{
				_response.Result = result;
				return Ok(_response);
			}
			else
			{
				throw new Exception("Not found!");
			}
		}
	}
}
