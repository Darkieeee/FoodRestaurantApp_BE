using AutoMapper;
using FoodRestaurantApp_BE.Constants;
using FoodRestaurantApp_BE.Exceptions;
using FoodRestaurantApp_BE.Filters;
using FoodRestaurantApp_BE.Models.Databases;
using FoodRestaurantApp_BE.Models.DTOs;
using FoodRestaurantApp_BE.Services;
using FoodRestaurantApp_BE.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace FoodRestaurantApp_BE.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ValidToken(AllowedRoles = [nameof(Roles.ADMIN)])]
    [ApiController]
    public class UserController(IUserService userService, IServiceProvider serviceProvider, 
                                IMapper mapper, ILogger<UserController> logger) : ControllerBase {
        private readonly IUserService _userService = userService;
        private readonly IMapper _mapper = mapper;
        private readonly ImageFileService _imageFileService = serviceProvider.GetRequiredService<ImageFileService>();
        private readonly ILogger<UserController> _logger = logger;

        [HttpPost("create")]
        public async Task<ActionResult> CreateUser([FromForm] CreateUserRequest request)
        {
            SystemUser user = _mapper.Map<SystemUser>(request);
            
            if(request.Avatar != null)
            {
                string mainDirectory = Environment.CurrentDirectory;
                string subDirectory = Path.Combine("Images", "Avatar");
                string fileUploadedUri = await _imageFileService.UploadFile(Path.Combine(mainDirectory, subDirectory), 
                                                                            request.Avatar);
                user.Avatar = fileUploadedUri;
            }

            try
            {
                DbDMLStatementResult result = await _userService.CreateAsync(user);
                return result.Success ? Ok(result) : BadRequest(result);
            }
            catch(SqlException)
            {
                if (user.Avatar != null && _imageFileService.CheckFileIfExists(user.Avatar))
                {
                    _imageFileService.DeleteFile(user.Avatar);
                }
                throw;
            }
            catch
            {
                throw;
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
