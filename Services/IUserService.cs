using FoodRestaurantApp_BE.Models.Dto;

namespace FoodRestaurantApp_BE.Services
{
    public interface IUserService
    {
        List<UserDto> GetAll();
        List<UserDto> GetUsersByName(string username);
        bool Authenticate(string username, string password);
    }
}
