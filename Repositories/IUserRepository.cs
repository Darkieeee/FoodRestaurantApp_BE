using FoodRestaurantApp_BE.Models.Dto;

namespace FoodRestaurantApp_BE.Repositories
{
    public interface IUserRepository: IDisposable {
        List<UserDto> GetAll();
        UserDto GetById(string id);
        UserDto Insert(UserDto t);
        UserDto Update(UserDto t);
        bool Delete(UserDto t);
        int Save();
    }
}
