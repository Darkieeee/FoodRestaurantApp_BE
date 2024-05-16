using FoodRestaurantApp_BE.Models.Databases;

namespace FoodRestaurantApp_BE.Repositories
{
    public interface IRoleRepository: IRepository<Role>
    {
        Role? FindById(int id);
    }
}
