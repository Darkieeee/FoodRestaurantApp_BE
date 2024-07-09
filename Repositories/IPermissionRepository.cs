using FoodRestaurantApp_BE.Models.Databases;

namespace FoodRestaurantApp_BE.Repositories
{
    public interface IPermissionRepository : IRepository<Permission>
    {
        IQueryable<Permission> FindByIds(List<string> ids);
    }
}
