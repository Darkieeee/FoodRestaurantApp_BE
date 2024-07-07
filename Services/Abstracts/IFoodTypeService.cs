using FoodRestaurantApp_BE.Models.Databases;
using FoodRestaurantApp_BE.Models.DTOs;

namespace FoodRestaurantApp_BE.Services.Abstracts
{
    public interface IFoodTypeService
    {
        List<FoodTypeModelResponse> GetAll();
        bool Create(FoodType type);
        bool Update(FoodType type);
        Task<bool> CreateAsync(FoodType type);
        Task<bool> UpdateAsync(FoodType type);
    }
}
