using FoodRestaurantApp_BE.Models.Databases;

namespace FoodRestaurantApp_BE.Repositories
{
    public interface IFoodTypeRepository : IRepository<FoodType>
    {
        IQueryable<FoodType> FindByName(string name);
        IQueryable<FoodType> FindById(int id);
        Task<bool> UpdateWithLock(int id, string name);
    }
}
