using FoodRestaurantApp_BE.DbContexts;
using FoodRestaurantApp_BE.Models.Databases;

namespace FoodRestaurantApp_BE.Repositories
{
    public class RoleRepository(FoodRestaurantDbContext dbContext) : IRoleRepository
    {
        private readonly FoodRestaurantDbContext _dbContext = dbContext;

        public int Delete(Role t)
        {
            throw new NotImplementedException();
        }

        public Role? FindById(int id)
        {
            IQueryable<Role> role = _dbContext.Roles.Where(x => x.Id.Equals(id)); 
            return role.FirstOrDefault();
        }

        public IQueryable<Role> GetAll()
        {
            throw new NotImplementedException();
        }

        public int Insert(Role t)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertAsync(Role t)
        {
            throw new NotImplementedException();
        }

        public int Update(Role t)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Role t)
        {
            throw new NotImplementedException();
        }
    }
}
