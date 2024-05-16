using FoodRestaurantApp_BE.DbContexts;
using FoodRestaurantApp_BE.Models.Databases;

namespace FoodRestaurantApp_BE.Repositories
{
    public class RoleRepository(FoodRestaurantDbContext dbContext) : IRoleRepository
    {
        private readonly FoodRestaurantDbContext _dbContext = dbContext;

        public bool Delete(Role t)
        {
            throw new NotImplementedException();
        }

        public Role? FindById(int id)
        {
            IQueryable<Role> role = _dbContext.Roles.Where(x => x.Id.Equals(id)); 
            return role.FirstOrDefault();
        }

        public List<Role> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Insert(Role t)
        {
            throw new NotImplementedException();
        }

        public bool Update(Role t)
        {
            throw new NotImplementedException();
        }
    }
}
