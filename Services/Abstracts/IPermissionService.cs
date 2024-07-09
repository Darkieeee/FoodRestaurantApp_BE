using FoodRestaurantApp_BE.Models.Databases;
using FoodRestaurantApp_BE.Repositories;

namespace FoodRestaurantApp_BE.Services.Abstracts
{
    public interface IPermissionService : IRepository<Permission>
    {
    }
}
