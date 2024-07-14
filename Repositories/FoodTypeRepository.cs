using FoodRestaurantApp_BE.DbContexts;
using FoodRestaurantApp_BE.Models.Databases;

namespace FoodRestaurantApp_BE.Repositories
{
    public class FoodTypeRepository(FoodRestaurantDbContext foodRestaurantDbContext) : Repository<FoodType>(foodRestaurantDbContext),
                                                                                       IFoodTypeRepository
    {
        public IQueryable<FoodType> FindById(int id)
        {
            return _dbContext.FoodTypes.Where(x => x.Id.Equals(id));
        }

        public IQueryable<FoodType> FindByName(string name)
        {
            return _dbContext.FoodTypes.Where(x => x.Name.Equals(name));
        }

        public Task<bool> UpdateWithLock(int id, string name)
        {
            throw new NotImplementedException();
        }
    }
}
