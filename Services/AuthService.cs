using FoodRestaurantApp_BE.Models.Databases;
using FoodRestaurantApp_BE.Models.Securities;
using FoodRestaurantApp_BE.Services.Abstracts;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FoodRestaurantApp_BE.Services
{
    public class AuthService(IUserService userService, IConfiguration configuration) : IAuthService
    {
        private readonly IUserService _userService = userService;
        private readonly IConfiguration _configuration = configuration;

        public AuthResult VerifyUser(string username, string password, bool isCustomer)
        {
            SystemUser? user = _userService.FindUsersByName(username).FirstOrDefault();
            AuthResult result = new();

            if (user == null)
            {
                result.Success = false;
                result.Message = "Username's not found";
            }
            else
            {
                if(!BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    result.Success = false;
                    result.Message = "Password is incorrect";
                }
                else if(IsInvalidRole(user, isCustomer))
                {
                    result.Success = false;
                    result.Message = "You are not authorized to access this action";
                }
                else
                {
                    string token = GenerateToken(user, _configuration["JwtBearer:SecretKey"]!);

                    result.Success = true;
                    result.Message = "User authentication is valid";
                    result.UserId = user.Id;
                    result.UserName = user.Name;
                    result.RoleName = user.Role.Name;
                    result.Token = token;
                }
            }

            return result;
        }

        private static bool IsInvalidRole(SystemUser user, bool isCustomer)
        {
            return (isCustomer && !user.RoleId.Equals(Constants.Roles.Customer))
                || (!isCustomer && user.RoleId.Equals(Constants.Roles.Customer));
        }

        private string GenerateToken(SystemUser user, string secretKey, int expiredMinutes = 10)
        {
            JwtSecurityTokenHandler _webTokenHandler = new();

            List<Claim> claims = [
                new(ClaimTypes.Sid, user.Id.ToString()),
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
    }
}
