using FoodRestaurantApp_BE.DbContexts;
using FoodRestaurantApp_BE.Models.Databases;
using FoodRestaurantApp_BE.Repositories;
using FoodRestaurantApp_BE.Services.Abstracts;

namespace FoodRestaurantApp_BE.Services
{
    public class PermissionService(FoodRestaurantDbContext dbContext) : Repository<Permission>(dbContext), 
                                                                        IPermissionService
    {
    }
}
