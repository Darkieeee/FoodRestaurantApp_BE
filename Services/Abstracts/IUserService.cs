using FoodRestaurantApp_BE.Models.Databases;

namespace FoodRestaurantApp_BE.Services.Abstracts
{
    public interface IUserService
    {
        List<SystemUser> GetAll();
        List<SystemUser> FindUsersByName(string username);
        bool Create(SystemUser user);
        bool Update(SystemUser user);
        Task<bool> UpdateAsync(SystemUser user);
        Task<bool> CreateAsync(SystemUser user);
    }
}
