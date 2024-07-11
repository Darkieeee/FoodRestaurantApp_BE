using FoodRestaurantApp_BE.Models.Databases;
using FoodRestaurantApp_BE.Models.DTOs;

namespace FoodRestaurantApp_BE.Services.Abstracts
{
    public interface IFoodService
    {
        OperationResult<Food> Create(Food f);
        Task<OperationResult<Food>> CreateAsync(Food f);
        List<FoodListView> GetAll();
    }
}
