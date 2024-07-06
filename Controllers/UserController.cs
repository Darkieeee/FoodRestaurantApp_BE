using AutoMapper;
using FoodRestaurantApp_BE.Filters;
using FoodRestaurantApp_BE.Models.Databases;
using FoodRestaurantApp_BE.Models.DTOs;
using FoodRestaurantApp_BE.Models.Requests;
using FoodRestaurantApp_BE.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodRestaurantApp_BE.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ValidToken]
    [ApiController]
    public class UserController(IUserService userService, IMapper mapper) : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly IMapper _mapper = mapper;

        [HttpPost("create")]
        public IActionResult CreateUser([FromBody] CreateUserRequest request)
        {
            SystemUser user = _mapper.Map<SystemUser>(request);
            bool created = _userService.Create(user);
            StatusResponse response = new()
            {
                Success = created
            };

            if (response.Success)
            {
                response.Message.Add("Thêm mới người dùng thành công");
                return Ok(response);
            }
            else
            {
                response.Message.Add("Thêm mới người dùng thất bại");
                return BadRequest(response);
            }
        }

        [HttpGet("get-pagination")]
        public IActionResult GetPagination(string? search, PageSizeOption option = PageSizeOption.TenPerPage, int page = 1)
        {
            return Ok(_userService.GetPagination(search, option, page));
        }

        [HttpGet("get-list")]
        public IActionResult GetList()
        {
            return Ok(); 
        }
    }
}
