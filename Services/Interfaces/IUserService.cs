using FoodRestaurantApp_BE.Models.Dto;

namespace FoodRestaurantApp_BE.Services.Interfaces
{
    public interface IUserService
    {
        List<UserDto> GetAll();
    }
}
