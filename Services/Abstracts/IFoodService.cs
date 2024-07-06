using FoodRestaurantApp_BE.Models.Databases;
using FoodRestaurantApp_BE.Models.DTOs;

namespace FoodRestaurantApp_BE.Services.Abstracts
{
    public interface IFoodService
    {
        Task<Food> AddAsync(CreateFoodRequest request);
    }
}
