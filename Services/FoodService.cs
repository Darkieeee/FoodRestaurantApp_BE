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
                Description = f.Description,
                MaxToppings = f.MaxToppings,
                Name = f.Name,
                Price = f.Price,
                Image = f.Image,
                FoodType = new FoodTypeModelResponse()
                {
                    Name = f.Name,
                    Slug = f.Slug,
                }
            };
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

        public FoodDetails GetBySlug(string slug)
        {
            throw new NotImplementedException();
        }

        public List<FoodBestSeller> TakeTopSellingFoods(int top)
        {
            return _foodRepository.FindBestSellingFoods(top).ToList();
        }
    }
}
