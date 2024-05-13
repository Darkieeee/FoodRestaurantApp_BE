using FoodRestaurantApp_BE.Models.Databases;
using FoodRestaurantApp_BE.Repositories;
using FoodRestaurantApp_BE.Services.Abstracts;

namespace FoodRestaurantApp_BE.Services
{
    public class UserService(IUserRepository userRepository) : IUserService {
        private readonly IUserRepository _userRepository = userRepository;

        public List<SystemUser> FindUsersByName(string username)
        {
            return _userRepository.FindByName(username);
        }

        List<SystemUser> IUserService.GetAll() {
            return _userRepository.GetAll();
        }
    }
}
