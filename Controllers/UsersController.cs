using FoodRestaurantApp_BE.Models.Dto;
using FoodRestaurantApp_BE.Requests;
using FoodRestaurantApp_BE.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FoodRestaurantApp_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserService userService, IConfiguration configuration) : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly IConfiguration _configuration = configuration;

        [HttpGet("/get-token")]
        public IActionResult GenerateToken([FromBody] GenerateTokenRequest request)
        {
            bool authenticated = _userService.Authenticate(request.Username, request.Password);
            
            if(!authenticated) 
            {
                return Unauthorized("Xác thực thất bại");
            }

            UserDto user = _userService.GetUsersByName(request.Username).First();
            string token = GenerateToken(user);

            return Ok(token);
        }

        private string GenerateToken(UserDto userDto, string secretKey = "", int expiredMinutes = 10)
        {
            JwtSecurityTokenHandler _webTokenHandler = new();
            
            List<Claim> claims = [
                new(ClaimTypes.Sid, userDto.Id.ToString()),
                new(ClaimTypes.Name, userDto.Name),
                new(ClaimTypes.Role, userDto.RoleId),
            ];

            byte[] secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);

            SigningCredentials credentials = new(
                new SymmetricSecurityKey(secretKeyBytes),
                SecurityAlgorithms.HmacSha512Signature
            );

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(expiredMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey)
            };

            SecurityToken token = _webTokenHandler.CreateToken(tokenDescriptor);

            return _webTokenHandler.WriteToken(token);
        }

        [HttpGet("create")]
        public IActionResult CreateUser(Requests.CreateUserRequest request)
        {
            return Ok();
        }
    }
}
