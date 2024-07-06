using FoodRestaurantApp_BE.Models.Databases;

namespace FoodRestaurantApp_BE.Repositories
{
    public interface IFoodRepository : IRepository<Food>
    {
        Food? FindById(int id);
    }
}
