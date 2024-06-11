using FoodRestaurantApp_BE.Models.Databases;
using FoodRestaurantApp_BE.Repositories;
using FoodRestaurantApp_BE.Services.Abstracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodRestaurantApp_BE.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        // Correct constructor definition
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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

        public List<SystemUser> GetAll()
        {
            return _userRepository.GetAll();
        }

        public SystemUser? GetUser(string name)
        {
            return _userRepository.FindUserByName(name);
        }

        public SystemUser? CreateUser(SystemUser user)
        {
            int result = _userRepository.Insert(user);

            if (result == 1)
            {
                return _userRepository.FindUserByName(user.Name);
            }
            return null;
        }
    }
}