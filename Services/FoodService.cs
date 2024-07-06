using FoodRestaurantApp_BE.Models.Databases;
using FoodRestaurantApp_BE.Models.DTOs;
using FoodRestaurantApp_BE.Repositories;
using FoodRestaurantApp_BE.Services.Abstracts;
using Slugify;

namespace FoodRestaurantApp_BE.Services
{
    public class FoodService(IFoodRepository foodRepository) : IFoodService
    {
        private readonly IFoodRepository _foodRepository = foodRepository;
       
        public async Task<Food> AddAsync(CreateFoodRequest request)
        {
            SlugHelper slugHelper = new();
            Food food = new()
            {
                Id = 0,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                TypeId = request.FoodType,
                Slug = slugHelper.GenerateSlug(request.Name),
            };
            await _foodRepository.InsertAsync(food);
            return food;
        }
    }
}
