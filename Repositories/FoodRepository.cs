using FoodRestaurantApp_BE.DbContexts;
using FoodRestaurantApp_BE.Models.Databases;

namespace FoodRestaurantApp_BE.Repositories
{
    public class FoodRepository(FoodRestaurantDbContext dbContext) : Repository<Food>(dbContext), 
                                                                     IFoodRepository
    {
        public Food? FindById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
