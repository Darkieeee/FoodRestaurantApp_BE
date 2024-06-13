using Azure;
using FoodRestaurantApp_BE.Models.DTOs;

namespace FoodRestaurantApp_BE.Services.Abstracts
{
    public interface IAuthService
    {
        AuthDto VerifyUser(string username, string password);
        bool SignUp(SignUpDto signUpDto);
        LogoutDto Logout(string tokenId);
        Task<AuthDto> VerifyUserAsync(string username, string password);
        Task<bool> SignUpAsync(SignUpDto signUpDto);
        Task<LogoutDto> LogoutAsync(string tokenId);
    }
}
