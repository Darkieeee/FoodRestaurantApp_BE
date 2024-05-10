using FoodRestaurantApp_BE.Models.Dto;

namespace FoodRestaurantApp_BE.Repositories
{
    public interface IUserRepository: IDisposable {
        List<UserDto> GetAll();
        UserDto FindById(string id);
        List<UserDto> FindByName(string username);
        UserDto? FindByNameAndPassword(string username, string password);
        bool Insert(UserDto t);
        bool Update(UserDto t);
        bool Delete(UserDto t);
    }
}
