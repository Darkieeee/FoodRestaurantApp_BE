using FoodRestaurantApp_BE.Models.Databases;
using FoodRestaurantApp_BE.Repositories;
using FoodRestaurantApp_BE.Services.Abstracts;

namespace FoodRestaurantApp_BE.Services
{
    public class RoleService(IRoleRepository repository) : IRoleService
    {
        private readonly IRoleRepository _repository = repository;

        public Role? FindById(int id)
        {
            if(id == 0) 
                throw new ArgumentException("Id must be greater than 0");
            return _repository.FindById(id);
        }
    }
}
