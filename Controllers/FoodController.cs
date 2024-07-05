using FoodRestaurantApp_BE.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FoodRestaurantApp_BE.Controllers
{
    [ApiController]
    public class FoodController : ControllerBase
    {
        [HttpPost("create")]
        public IActionResult CreateFood([FromBody] CreateFoodRequest request)
        {
            return Ok(request);
        }
    }
}
