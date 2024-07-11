using FoodRestaurantApp_BE.Models.Databases;
using FoodRestaurantApp_BE.Models.DTOs;
using FoodRestaurantApp_BE.Repositories;
using FoodRestaurantApp_BE.Services.Abstracts;

namespace FoodRestaurantApp_BE.Services
{
    public class FoodService(IFoodRepository foodRepository) : IFoodService
    {
        private readonly IFoodRepository _foodRepository = foodRepository;

        public OperationResult<Food> Create(Food f)
        {
            return CreateAsync(f).Result;
        }

        public async Task<OperationResult<Food>> CreateAsync(Food f)
        {
            OperationResult<Food> result = new();
            try
            {
                bool created = await _foodRepository.InsertAsync(f) > 0;

                if (created)
                {
                    result.Success = true;
                    result.Message = "Add new food successfully";
                    result.Value = f;
                }
                else
                {
                    result.Success = false;
                    result.Message = "Add new food unsuccessfully";
                }
            } catch(Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.Exception = ex;
            }
            
            return result;
        }

        private static FoodListView ToListView(Food f)
        {
            return new FoodListView()
            {
                Name = f.Name,
                Price = f.Price,
                Description = f.Description,
                Image = f.Image,
                MaxToppings = f.MaxToppings
            };
        }

        public List<FoodListView> GetAll()
        {
            return _foodRepository.GetAll().Select(x => ToListView(x)).ToList();
        }
    }
}
