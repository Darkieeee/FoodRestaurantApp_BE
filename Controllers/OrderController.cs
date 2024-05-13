using Microsoft.AspNetCore.Mvc;
using FoodRestaurantApp_BE.Services.Abstracts;

namespace FoodRestaurantApp_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IOrderSystemService orderSystemService) : ControllerBase() {
        private readonly IOrderSystemService _orderSystemService = orderSystemService;

        [HttpGet("detail")]
        public IActionResult GetOrderDetail(string orderId) {
            return Ok(_orderSystemService.GetDetail(orderId));
        }
    }
}
