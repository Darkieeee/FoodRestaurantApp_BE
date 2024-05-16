using FoodRestaurantApp_BE.Models.Databases;

namespace FoodRestaurantApp_BE.Services.Abstracts
{
    public interface IRoleService
    {
        Role? FindById(int id);
    }
}
