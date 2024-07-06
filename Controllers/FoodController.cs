using FoodRestaurantApp_BE.Filters;
using FoodRestaurantApp_BE.Models.DTOs;
using FoodRestaurantApp_BE.Services;
using FoodRestaurantApp_BE.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodRestaurantApp_BE.Controllers
{
    [ApiController]
    [Authorize]
    [ValidToken]
    public class FoodController(IFoodService foodService) : ControllerBase
    {
        private readonly IFoodService _foodService = foodService;

        [HttpPost("create")]
        public IActionResult CreateFood([FromBody] CreateFoodRequest request)
        {
            return Ok(_foodService.AddAsync(request));
        }
    }
}
