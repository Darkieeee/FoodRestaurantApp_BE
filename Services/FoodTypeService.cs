using FoodRestaurantApp_BE.Exceptions;
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

        public bool Delete(int id)
        {
            return DeleteAsync(id).Result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            FoodType? foodType = _foodTypeRepository.FindById(id).FirstOrDefault();
            return foodType is null ? throw new NotFoundException($"Not found food type with id '{id}'", null) 
                                    : await _foodTypeRepository.DeleteAsync(foodType) > 0;
        }

        public List<FoodTypeListView> GetAll()
        {
            return _foodTypeRepository.GetAll().Select(x => ToListView(x)).ToList();
        }

        private static FoodTypeListView ToListView(FoodType type)
        {
            return new FoodTypeListView()
            {
                Id = type.Id,
                Name = type.Name,
                Slug = type.Slug,
            };
        }

        public FoodTypeListView GetById(int id)
        {
            FoodType? foodType = _foodTypeRepository.FindById(id).FirstOrDefault();
            return foodType is null ? throw new NotFoundException($"Not found food type with id '{id}'", null) : ToListView(foodType);
        }

        public bool Update(FoodType type)
        {
            return UpdateAsync(type).Result;
        }

        public async Task<bool> UpdateAsync(FoodType type)
        {
            var nameOverlaps = _foodTypeRepository.FindByName(type.Name).Any();
            return nameOverlaps ? throw new ValueAlreadyExistsException($"The food type with name '{type.Name}' has already existed") 
                                : await _foodTypeRepository.UpdateAsync(type) > 0;
        }
    }
}
