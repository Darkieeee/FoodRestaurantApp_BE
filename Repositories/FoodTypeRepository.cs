using FoodRestaurantApp_BE.DbContexts;
using FoodRestaurantApp_BE.Models.Databases;

namespace FoodRestaurantApp_BE.Repositories
{
    public class FoodTypeRepository(FoodRestaurantDbContext foodRestaurantDbContext) : Repository<FoodType>(foodRestaurantDbContext),
                                                                                       IFoodTypeRepository
    {
        
    }
}
