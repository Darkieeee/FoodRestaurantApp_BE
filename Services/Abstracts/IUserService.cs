using FoodRestaurantApp_BE.Models.Databases;

namespace FoodRestaurantApp_BE.Services.Abstracts
{
    public interface IUserService
    {
        List<SystemUser> GetAll();
        List<SystemUser> FindUsersByName(string username);
        bool Update(SystemUser user);
        Task<bool> UpdateAsync(SystemUser user);
    }
}
