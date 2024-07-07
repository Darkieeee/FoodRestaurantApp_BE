using AutoMapper;
using FoodRestaurantApp_BE.Constants;
using FoodRestaurantApp_BE.Exceptions;
using FoodRestaurantApp_BE.Filters;
using FoodRestaurantApp_BE.Models.Databases;
using FoodRestaurantApp_BE.Models.DTOs;
using FoodRestaurantApp_BE.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodRestaurantApp_BE.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ValidToken(AllowedRoles = [nameof(Roles.ADMIN)])]
    [ApiController]
    public class UserController(IUserService userService, IMapper mapper) : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly IMapper _mapper = mapper;

        [HttpPost("create")]
        public async Task<ActionResult> CreateUser([FromForm] CreateUserRequest request)
        {
            SystemUser user = _mapper.Map<SystemUser>(request);
            bool created = await _userService.CreateAsync(user);
            StatusResponse response = new()
            {
                Success = created
            };

            if (response.Success)
            {
                response.Messages.Add("Add new user successfully");
                return Ok(response);
            }
            else
            {
                response.Messages.Add("Add new user unsuccessfully");
                return BadRequest(response);
            }
        }

        [HttpGet("uuid/{uuid}")]
        public IActionResult GetByUuid(string uuid)
        {
            UserDetailModelResponse? userDetail = _userService.GetByUuid(uuid);
            
            if(userDetail != null)
            {
                return Ok(userDetail);
            }
            else 
            {
                throw new NotFoundException($"Not found user with uuid {uuid}", null);
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
            return Ok(_userService.GetAll()); 
        }
    }
}
