using FoodRestaurantApp_BE.Models.Databases;

namespace FoodRestaurantApp_BE.Repositories
{
    public interface IRolePermissionRepository : IRepository<RolePermission>
    {
        IQueryable<RolePermission> FindByRoleId(int roleId);
        IQueryable<RolePermission> FindByRoleIds(List<int> roleIds);
    }
}
