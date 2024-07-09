using FoodRestaurantApp_BE.DbContexts;
using FoodRestaurantApp_BE.Models.Databases;

namespace FoodRestaurantApp_BE.Repositories
{
    public class RolePermissionRepository(FoodRestaurantDbContext dbContext) : Repository<RolePermission>(dbContext),
                                                                               IRolePermissionRepository
    {
        public IQueryable<RolePermission> FindByRoleId(int roleId)
        {
            return _dbContext.RolesPermissions.Where(x => x.RoleId.Equals(roleId));
        }

        public IQueryable<RolePermission> FindByRoleIds(List<int> roleIds)
        {
            return _dbContext.RolesPermissions.Where(x => roleIds.Contains(x.RoleId));
        }
    }
}
