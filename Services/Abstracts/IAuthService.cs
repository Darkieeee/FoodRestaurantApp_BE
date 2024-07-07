using Azure;
using FoodRestaurantApp_BE.Models.DTOs;

namespace FoodRestaurantApp_BE.Services.Abstracts
{
    public interface IAuthService
    {
        AuthResponse VerifyUser(string username, string password);
        RegisterDto SignUp(SignUpDto signUpDto);
        LogoutDto Logout(string tokenId);
        Task<AuthResponse> VerifyUserAsync(string username, string password);
        Task<RegisterDto> SignUpAsync(SignUpDto signUpDto);
        Task<LogoutDto> LogoutAsync(string tokenId);
    }
}
