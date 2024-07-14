using FoodRestaurantApp_BE.Models.Databases;
using FoodRestaurantApp_BE.Models.DTOs;

namespace FoodRestaurantApp_BE.Services.Abstracts
{
    public interface IFoodService
    {
        List<FoodBestSeller> TakeTopSellingFoods(int top);
        FoodDetails GetBySlugAndId(string slug, int id);
        OperationResult<Food> Create(Food f);
        Task<OperationResult<Food>> CreateAsync(Food f);
        List<FoodListView> GetAll();
        Pagination<FoodDetails> GetPagination(string? search, PageSizeOption pageSizeOption, int currentPage);
        Task<bool> DeleteAsync(int id);
    }
}
