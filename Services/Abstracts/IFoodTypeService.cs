using FoodRestaurantApp_BE.Models.Databases;
using FoodRestaurantApp_BE.Models.DTOs;

namespace FoodRestaurantApp_BE.Services.Abstracts
{
    public interface IFoodTypeService
    {
        List<FoodTypeListView> GetAll();
        FoodTypeListView GetById(int id);
        bool Create(FoodType type);
        bool Update(FoodType type);
        bool Delete(int id);
        Task<bool> CreateAsync(FoodType type);
        Task<bool> UpdateAsync(FoodType type);
        Task<bool> DeleteAsync(int id);
    }
}
