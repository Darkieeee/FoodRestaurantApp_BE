using FoodRestaurantApp_BE.Models.Databases;
using FoodRestaurantApp_BE.Repositories;
using FoodRestaurantApp_BE.Services.Abstracts;

namespace FoodRestaurantApp_BE.Services
{
    public class UserService(IUserRepository userRepository) : IUserService {
        private readonly IUserRepository _userRepository = userRepository;

        public bool Create(SystemUser user)
        {
            return CreateAsync(user).Result;
        }

        public async Task<bool> CreateAsync(SystemUser user)
        {
            return await _userRepository.InsertAsync(user) > 0;
        }

        public List<SystemUser> FindUsersByName(string username)
        {
            return _userRepository.FindByName(username);
        }

        public bool Update(SystemUser user)
        {
            return UpdateAsync(user).Result;
        }

        public async Task<bool> UpdateAsync(SystemUser user)
        {
            return await _userRepository.UpdateAsync(user) > 0;
        }

        List<SystemUser> IUserService.GetAll() {
            return _userRepository.GetAll();
        }
    }
}
