using FoodRestaurantApp_BE.Models.Databases;
using FoodRestaurantApp_BE.Models.DTOs;

namespace FoodRestaurantApp_BE.Repositories
{
    public interface IFoodRepository : IRepository<Food>
    {
        IQueryable<Food> FindById(int id);
        IQueryable<Food> FindBySlug(string slug);
        IQueryable<FoodBestSeller> FindBestSellingFoods(int top);
        IQueryable<Food> FindRelatedFoods(string slug);
    }
}
