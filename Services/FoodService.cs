using FoodRestaurantApp_BE.Exceptions;
using FoodRestaurantApp_BE.Models.Databases;
using FoodRestaurantApp_BE.Models.DTOs;
using FoodRestaurantApp_BE.Repositories;
using FoodRestaurantApp_BE.Services.Abstracts;
using Microsoft.EntityFrameworkCore;

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

        private static FoodDetails ToDetails(Food f)
        {
            return new FoodDetails()
            {
                Id = f.Id,
                Description = f.Description,
                MaxToppings = f.MaxToppings,
                Name = f.Name,
                Price = f.Price,
                Image = f.Image,
                Url = $"{f.Slug}-fid{f.Id}",
                FoodType = new FoodTypeListView()
                {
                    Id = f.Id,
                    Name = f.Name,
                    Slug = f.Slug,
                }
            };
        } 

        private static FoodListView ToListView(Food f)
        {
            return new FoodListView()
            {
                Id = f.Id,
                Name = f.Name,
                Price = f.Price,
                Description = f.Description,
                Image = f.Image,
                MaxToppings = f.MaxToppings,
                Url = $"{f.Slug}-fid{f.Id}"
            };
        }

        public List<FoodListView> GetAll()
        {
            return _foodRepository.GetAll().Select(x => ToListView(x)).ToList();
        }

        public Pagination<FoodDetails> GetPagination(string? search, PageSizeOption pageSizeOption, int currentPage)
        {
            var foods = _foodRepository.GetAll();

            if (!string.IsNullOrEmpty(search))
            {
                foods = foods.Where(x => x.Name.Contains(search!));
            }

            int totalCount = foods.Count();

            int pageSize = (int) pageSizeOption;
            int totalPage = (int) Math.Ceiling((double) totalCount / pageSize);
            int skipRows = (currentPage - 1) * pageSize;

            List<FoodDetails> FoodDetails = foods.OrderBy(x => x.Name)
                                                 .Skip(skipRows).Take(pageSize)
                                                 .Include(x => x.Type)
                                                 .Select(x => ToDetails(x))
                                                 .ToList();

            Pagination<FoodDetails> pagination;
            pagination = PaginationHelper.CreateBuilder<FoodDetails>()
                                         .WithCurrentPage(currentPage)
                                         .WithPageSize(pageSizeOption)
                                         .WithData(FoodDetails, totalCount, totalPage)
                                         .Build();

            return pagination;
        }

        public FoodDetails GetBySlugAndId(string slug, int id)
        {
            Food? food = _foodRepository.FindBySlugAndId(slug, id).FirstOrDefault();
            return food is null ? throw new NotFoundException($"Not found food with slug '{slug}' and id '{id}'", null) : ToDetails(food);
        }

        public List<FoodBestSeller> TakeTopSellingFoods(int top)
        {
            return _foodRepository.FindBestSellingFoods(top).ToList();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Food? food = _foodRepository.FindById(id).FirstOrDefault();
            return food is null ? throw new NotFoundException($"Not found food with id '{id}'", null)
                                : await _foodRepository.DeleteAsync(food) > 0;
        }
    }
}
