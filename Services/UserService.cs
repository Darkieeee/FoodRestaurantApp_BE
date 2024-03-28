using FoodRestaurantApp_BE.Contexts;
using FoodRestaurantApp_BE.Models.Dto;
using FoodRestaurantApp_BE.Services.Interfaces;

namespace FoodRestaurantApp_BE.Services {
    public class UserService(FoodRestaurantDbContext dbContext) : IUserService {
        private readonly FoodRestaurantDbContext _dbContext = dbContext;
        List<UserDto> IUserService.GetAll() {
            return _dbContext.Users.ToList();
        }
    }
}
