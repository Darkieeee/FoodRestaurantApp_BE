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
                             IDistributedCache cache) : IAuthService
    {
        private readonly IUserService _userService = userService;
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
            SystemUser? user = _userService.FindUsersByName(username).FirstOrDefault();
            AuthDto result = new();

            if (user == null)
            {
                result.Success = false;
                result.Code = AuthDto.WRONG_USERNAME;
                result.Message = "Sai tên đăng nhập";
            }
            else
            {
                if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    result.Success = false;
                    result.Code = AuthDto.WRONG_PASSWORD;
                    result.Message = "Sai mật khẩu";
                }
                else
                {
                    string token = GenerateToken(user, _configuration["JwtBearer:SecretKey"]!);

                    user.LastLogin = DateTime.Now;

                    result.Success = true;
                    result.Code = AuthDto.IS_VALID;
                    result.Message = "Xác thực hợp lệ";
                    result.UserId = user.Id;
                    result.UserName = user.Name;
                    result.RoleName = user.Role.Description;
                    result.IsAdmin = user.Role.Id != Constants.Roles.Customer;
                    result.Token = token;

                    await _cache.SetRecordAsync(token, true);
                    await _userService.UpdateAsync(user);
                }
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

        public async Task<LogoutDto> LogoutAsync(string tokenId)
        {
            LogoutDto logoutResult = new();
            
            try
            {
                await _cache.SetRecordAsync(tokenId, false);
                
                logoutResult.Success = true;
                logoutResult.Message = "Đăng xuất thành công";
            } 
            catch 
            {
                logoutResult.Success = false;
                logoutResult.Message = "Đã xảy ra lỗi! Vui lòng thử lại sau";
            }

            return logoutResult;
        }
    }
}
