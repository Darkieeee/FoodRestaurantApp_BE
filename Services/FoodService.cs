using FoodRestaurantApp_BE.Models.Databases;
using FoodRestaurantApp_BE.Repositories;
using FoodRestaurantApp_BE.Services.Abstracts;

namespace FoodRestaurantApp_BE.Services
{
    public class FoodService(IFoodRepository foodRepository) : IFoodService
    {
        private readonly IFoodRepository _foodRepository = foodRepository;

        public bool Create(Food f)
        {
            return CreateAsync(f).Result;
        }

        public async Task<bool> CreateAsync(Food f)
        {
            return await _foodRepository.InsertAsync(f) > 0;
        }
    }
}
