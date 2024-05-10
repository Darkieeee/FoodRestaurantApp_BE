using FoodRestaurantApp_BE.Models.Dto;
using FoodRestaurantApp_BE.Repositories;

namespace FoodRestaurantApp_BE.Services
{
    public class UserService(IUserRepository userRepository) : IUserService {
        private readonly IUserRepository _userRepository = userRepository;

        public bool Authenticate(string username, string password)
        {
            UserDto? user = _userRepository.FindByNameAndPassword(username, password);
            return (user != null);
        }

        public List<UserDto> GetUsersByName(string username)
        {
            return _userRepository.FindByName(username);
        }

        List<UserDto> IUserService.GetAll() {
            return _userRepository.GetAll();
        }
    }
}
