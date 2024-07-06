using FoodRestaurantApp_BE.DbContexts;
using FoodRestaurantApp_BE.Models.Databases;

namespace FoodRestaurantApp_BE.Repositories
{
    public class RoleRepository(FoodRestaurantDbContext dbContext) : Repository<Role>(dbContext),
                                                                     IRoleRepository
    {
        public Role? FindById(int id)
        {
            IQueryable<Role> role = _dbContext.Roles.Where(x => x.Id.Equals(id)); 
            return role.FirstOrDefault();
        }
    }
}
