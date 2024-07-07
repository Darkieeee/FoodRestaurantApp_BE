using FoodRestaurantApp_BE.Models.Databases;

namespace FoodRestaurantApp_BE.Services.Abstracts
{
    public interface IFoodService
    {
        bool Create(Food f);
        Task<bool> CreateAsync(Food f);
    }
}
