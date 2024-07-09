using FoodRestaurantApp_BE.Models.Databases;

namespace FoodRestaurantApp_BE.Repositories
{
    public interface IRoleRepository: IRepository<Role>
    {
        IQueryable<Role> FindById(int id);
        IQueryable<Role> FindByName(string name);
        IQueryable<Role> FindByIds(List<int> ids);
    }
}
