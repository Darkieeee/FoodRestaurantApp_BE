using FoodRestaurantApp_BE.DbContexts;
using FoodRestaurantApp_BE.Models.Databases;

namespace FoodRestaurantApp_BE.Repositories
{
    public class PermissionRepository(FoodRestaurantDbContext dbContext) : Repository<Permission>(dbContext),
                                                                           IPermissionRepository
    {
        public IQueryable<Permission> FindByIds(List<string> ids)
        {
            return _dbContext.Permissions.Where(x => ids.Contains(x.Id));
        }
    }
}
