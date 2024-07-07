using FoodRestaurantApp_BE.Models.Databases;
using FoodRestaurantApp_BE.Models.DTOs;
using FoodRestaurantApp_BE.Repositories;
using FoodRestaurantApp_BE.Services.Abstracts;

namespace FoodRestaurantApp_BE.Services
{
    public class FoodTypeService(IFoodTypeRepository foodTypeRepository) : IFoodTypeService
    {
        private readonly IFoodTypeRepository _foodTypeRepository = foodTypeRepository;

        public bool Create(FoodType type)
        {
            return CreateAsync(type).Result;
        }

        public async Task<bool> CreateAsync(FoodType type)
        {
            return await _foodTypeRepository.InsertAsync(type) > 0;
        }

        public List<FoodTypeModelResponse> GetAll()
        {
            return _foodTypeRepository.GetAll().Select(x => new FoodTypeModelResponse() {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
            }).ToList();
        }

        public bool Update(FoodType type)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(FoodType type)
        {
            throw new NotImplementedException();
        }
    }
}
