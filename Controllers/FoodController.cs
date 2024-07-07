using AutoMapper;
using FoodRestaurantApp_BE.Filters;
using FoodRestaurantApp_BE.Models.Databases;
using FoodRestaurantApp_BE.Models.DTOs;
using FoodRestaurantApp_BE.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodRestaurantApp_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ValidToken]
    public class FoodController(IFoodService foodService, IMapper mapper) : ControllerBase
    {
        private readonly IFoodService _foodService = foodService;
        private readonly IMapper _mapper = mapper;

        [HttpPost("create")]
        public IActionResult CreateFood([FromBody] CreateFoodRequest request)
        {
            Food f = _mapper.Map<Food>(request);
            return Ok(_foodService.CreateAsync(f));
        }
    }
}
