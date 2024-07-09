using FoodRestaurantApp_BE.DbContexts;
using FoodRestaurantApp_BE.Models.Databases;

namespace FoodRestaurantApp_BE.Repositories
{
    public class RoleRepository(FoodRestaurantDbContext dbContext) : Repository<Role>(dbContext),
                                                                     IRoleRepository
    {
        public IQueryable<Role> FindById(int id)
        {
            return _dbContext.Roles.Where(x => x.Id.Equals(id)); 
        }

        public IQueryable<Role> FindByIds(List<int> ids)
        {
            return _dbContext.Roles.Where(x => ids.Contains(x.Id));
        }

        public IQueryable<Role> FindByName(string name)
        {
            return _dbContext.Roles.Where(x => x.Name.Equals(name));
        }
    }
}
