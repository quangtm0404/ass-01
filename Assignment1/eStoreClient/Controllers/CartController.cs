using eStoreClient.Models.OrderDTO;
using eStoreClient.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace eStoreClient.Controllers
{
    public class CartController : Controller
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IOrderService _orderService;
        public CartController(IMemoryCache memCache, IOrderService orderService)
        {
            _memoryCache = memCache;
            _orderService = orderService;
        }


        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            var cart = _memoryCache.Get<OrderCreateDTO>(userId!);
            if (cart is not null)
            {
                if (cart.OrderDetails is not null)
                {
                    return View(cart);
                }
            }
            TempData["error"] = "You have not add anything to cart!";
            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public async Task<IActionResult> CheckOut()
        {
            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub)!;
            var cart = _memoryCache.Get<OrderCreateDTO>(userId);
            if (cart is not null)
            {
                var result = await _orderService.CreateOrder(cart);
                if (result is not null && result.IsSuccess)
                {
                    var res = JsonConvert.DeserializeObject<OrderViewDTO>(Convert.ToString(result.Result)!);
                    TempData["success"] = "Check Out Successfully!";
                    return RedirectToAction("Details", "Order", new { id = res!.Id });
                }
            }
            TempData["error"] = "Check out Failed! Please try again!";
            return RedirectToAction("Index");

        }
        [HttpPost]
        public IActionResult Update(Guid id, int quantity)
        {
            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub)!;
            var cart = _memoryCache.Get<OrderCreateDTO>(userId);
            if (cart is not null)
            {
                var item = cart.OrderDetails.FirstOrDefault(x => x.ProductId == id);
                if (item is not null)
                {
                    item.Quantity = quantity;
                    double total = 0;
                    cart.OrderDetails.ToList().ForEach((x) =>
                    {
                        total += x.Quantity * x.UnitPrice;
                    });
                    cart.Total = total;
                    _memoryCache.Set(userId, cart);
                    TempData["success"] = "Update Quantity Successfully!";
                    return RedirectToAction(nameof(Index));
                }
            }
            TempData["error"] = "Update Item Failed! Please Try Again";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Remove(Guid id)
        {
            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub)!;
            var cart = _memoryCache.Get<OrderCreateDTO>(userId);
            if (cart is not null)
            {
                var item = cart.OrderDetails.FirstOrDefault(x => x.ProductId == id);
                if (item is not null)
                {
                    cart.OrderDetails.Remove(item);
                    TempData["success"] = $"Remove {item.ProductName} from cart successfully!";
                    _memoryCache.Set(userId, cart);
                    return RedirectToAction(nameof(Index));
                }
            }
            TempData["error"] = "Remove Item Failed! Please Try Again";
            return RedirectToAction("Index");
        }

    }
}
