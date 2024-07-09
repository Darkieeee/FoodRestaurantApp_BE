using AutoMapper;
using FluentValidation;
using FoodRestaurantApp_BE.Constants;
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
    public class UserController(IUserService userService, IFileService fileService, 
                                IMapper mapper, ILogger<UserController> logger) : ControllerBase {
        private readonly IUserService _userService = userService;
        private readonly IMapper _mapper = mapper;
        private readonly IFileService _fileService = (ImageFileService) fileService;
        private readonly ILogger<UserController> _logger = logger;

        [HttpPost("create")]
        public async Task<ActionResult> CreateUser([FromForm] CreateUserRequest request, IValidator<CreateUserRequest> validator)
        {
            var validated = validator.Validate(request);
            if (!validated.IsValid) 
            {
                StatusResponse errorResponse = new()
                {
                    Success = false,
                    Messages = validated.Errors.Select(x => x.ErrorMessage).ToList()
                };
                return BadRequest(errorResponse);
            }

            SystemUser user = _mapper.Map<SystemUser>(request);
            
            if(request.Avatar != null)
            {
                string mainDirectory = Environment.CurrentDirectory;
                string subDirectory = Path.Combine("Images", "Avatar");
                string fileUploadedUri = await _fileService.UploadFile(Path.Combine(mainDirectory, subDirectory), 
                                                                       request.Avatar);
                user.Avatar = fileUploadedUri;
            }

            try
            {
                OperationResult result = await _userService.CreateAsync(user);
                return result.Success ? Ok(result) : BadRequest(result);
            }
            catch(SqlException)
            {
                if (user.Avatar != null && _fileService.CheckFileIfExists(user.Avatar))
                {
                    _fileService.DeleteFile(user.Avatar);
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
            UserDetails userDetail = _userService.GetByUuid(uuid);
            return Ok(userDetail);
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
