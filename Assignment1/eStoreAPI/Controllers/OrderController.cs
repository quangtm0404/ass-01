using Microsoft.AspNetCore.Mvc;
using Services.Services.Interfaces;
using Services.ViewModels;
using Services.ViewModels.OrderDTO;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private ResponseDTO _response;
        public OrderController(IOrderService orderService)
        {

            _orderService = orderService;
            _response = new();
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _orderService.GetAllOrders();
            if (result is not null && result.Count() > 0)
            {
                _response.Result = result;
                return Ok(_response);
            }
            else
            {
                throw new Exception("Not have any Orders");
            }
        }
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var result = _orderService.GetOrderById(id);
            if (result is not null)
            {
                _response.Result = result;
                return Ok(_response);
            }
            else throw new Exception("Not found!");
        }


        [HttpPost]
        public IActionResult Post(OrderCreateDTO model)
        {
            var result = _orderService.CreateOrder(model);
            if (result is not null)
            {
                _response.Result = result;
                return Ok(_response);
            }
            else throw new Exception("Create failed!");
        }

        [HttpGet("statistic")]
        public IActionResult GetByDate([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var result = _orderService.GetByDates(startDate, endDate);
            if (result is not null)
            {
                _response.Result = result;
                return Ok(_response);
            }
            else throw new Exception("Not found any orders");
        }
    }
}
