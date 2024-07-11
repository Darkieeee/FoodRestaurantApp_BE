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

        public IQueryable<RolePermission> FindByRoleName(string roleName)
        {
            var roles = _dbContext.Roles.Where(x => x.Name.Equals(roleName)).Select(x => x.Id);
            return _dbContext.RolesPermissions.Join(roles,
                                                    left => left.RoleId, right => right,
                                                    (left, right) => left);
        }
    }
}
