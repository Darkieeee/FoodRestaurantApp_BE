using FoodRestaurantApp_BE.Helpers;
using FoodRestaurantApp_BE.Models.Databases;
using FoodRestaurantApp_BE.Models.DTOs;
using FoodRestaurantApp_BE.Services.Abstracts;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FoodRestaurantApp_BE.Services
{
    public class AuthService(IUserService userService, IConfiguration configuration, 
                             IRoleService roleService, IDistributedCache cache) : IAuthService
    {
        private readonly IUserService _userService = userService;
        private readonly IRoleService _roleService = roleService;
        private readonly IConfiguration _configuration = configuration;
        private readonly IDistributedCache _cache = cache;

        public AuthDto VerifyUser(string username, string password)
        {
            return VerifyUserAsync(username, password).Result;
        }
        
        public LogoutDto Logout(string token)
        {
            return LogoutAsync(token).Result;
        }

        public async Task<AuthDto> VerifyUserAsync(string username, string password)
        {
            SystemUser? user = await _userService.Authenticate(username, password);
            AuthDto result = new();

            if (user != null)
            {
                if(!user.IsActive)
                {
                    result.Success = false;
                    result.Message = "User is inactive. Please contact admin for more information";
                }

                else
                {
                    string token = GenerateToken(user, _configuration["JwtBearer:SecretKey"]!);

                    result.Success = true;
                    result.Message = "Valid authentication";
                    result.UserId = user.Id;
                    result.UserName = user.Name;
                    result.RoleName = user.Role.Description;
                    result.IsAdmin = user.Role.Id != Constants.Roles.Customer;
                    result.Token = token;
                }
            }
            else
            {
                result.Success = false;
                result.Message = "Wrong username or password";
            }

            return result;
        }

        private string GenerateToken(SystemUser user, string secretKey, int expiredMinutes = 10)
        {
            JwtSecurityTokenHandler _webTokenHandler = new();

            List<Claim> claims = [
                new("uid", user.Id.ToString()),
                new(ClaimTypes.Name, user.Name),
                new(ClaimTypes.Role, user.Role.Name),
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
                SigningCredentials = credentials
            };

            SecurityToken token = _webTokenHandler.CreateToken(tokenDescriptor);

            return _webTokenHandler.WriteToken(token);
        }

        public async Task<RegisterDto> SignUpAsync(SignUpDto request)
        {
            RegisterDto result = new();

            if(_userService.CheckUsernameIfExists(request.Username))
            {
                result.Success = false;
                result.Message = "Username has already existed";

                return result;
            }

            if (_userService.CheckEmailIfExists(request.Username))
            {
                result.Success = false;
                result.Message = "Email has already existed";

                return result;
            }

            SystemUser user = new()
            {
                Name = request.Username,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                FullName = request.FullName,
                RoleId = Constants.Roles.Customer,
                IsActive = true,
            };

            bool registered = await _userService.CreateAsync(user);

            if(registered) {
                result.Success = true;
                result.Message = "Registration succeeded";
            } else{
                result.Success = false;
                result.Message = "Registration failed";
            }
            return result;
        }

        public async Task<LogoutDto> LogoutAsync(string tokenId)
        {
            LogoutDto logoutResult = new();

            await _cache.SetRecordAsync(tokenId, true);

            logoutResult.Success = true;
            logoutResult.Message = "Logout succeeded";
      
            return logoutResult;
        }

        public RegisterDto SignUp(SignUpDto signUpDto)
        {
            return SignUpAsync(signUpDto).Result;
        }
    }
}
