using FoodRestaurantApp_BE.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodRestaurantApp_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase {
        private readonly IUserService _userService = userService;

        [HttpGet]
        public IActionResult GetAllSystemUsers() {
            return Ok(_userService.GetAll());
        }
    }
}
