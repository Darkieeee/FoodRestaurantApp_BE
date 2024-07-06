using FoodRestaurantApp_BE.Models.Databases;
using FoodRestaurantApp_BE.Models.DTOs;

namespace FoodRestaurantApp_BE.Repositories
{
    public interface IUserRepository: IRepository<SystemUser>
    {
        IQueryable<SystemUser> FindById(int id);
        IQueryable<SystemUser> FindByName(string username);
        IQueryable<SystemUser> FindByEmail(string email);
    }
}
