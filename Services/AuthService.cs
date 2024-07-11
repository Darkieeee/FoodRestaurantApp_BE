using FoodRestaurantApp_BE.Models.Databases;
using FoodRestaurantApp_BE.Models.DTOs;
using FoodRestaurantApp_BE.Services.Abstracts;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FoodRestaurantApp_BE.Services
{
    public class AuthService(IUserService userService, IConfiguration configuration, 
                             IRoleService roleService, ITokenBlacklistService tokenBlacklistService) : IAuthService
    {
        private readonly IUserService _userService = userService;
        private readonly IRoleService _roleService = roleService;
        private readonly IConfiguration _configuration = configuration;
        private readonly ITokenBlacklistService _tokenBlacklstService = tokenBlacklistService;

        public AuthResponse VerifyUser(string username, string password, bool isAdmin)
        {
            return VerifyUserAsync(username, password, isAdmin).Result;
        }
        
        public LogoutDto Logout(string token)
        {
            return LogoutAsync(token).Result;
        }

        public async Task<AuthResponse> VerifyUserAsync(string username, string password, bool isAdmin)
        {
            UserShortDetails? user = await _userService.Authenticate(username, password, isAdmin);
            AuthResponse result = new();

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
                    result.UserName = user.Name;
                    result.IsAdmin = isAdmin;
                    result.RoleName = user.Role.Description;
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

        private string GenerateToken(UserShortDetails user, string secretKey, int expiredMinutes = 10)
        {
            JwtSecurityTokenHandler _webTokenHandler = new();

            List<Claim> claims = [
                new(ClaimTypes.Actor, user.Uuid),
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

            SystemUser user = new()
            {
                Name = request.Username,
                Email = request.Email,
                Uuid = Guid.NewGuid().ToString(),
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                FullName = request.FullName,
                RoleId = (int) Constants.Roles.KHHANG,
                IsActive = true,
                IsAdmin = false
            };

            OperationResult<SystemUser> statementResult = await _userService.CreateAsync(user);

            if(statementResult.Success) {
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

            await _tokenBlacklstService.AddToken(tokenId);

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
