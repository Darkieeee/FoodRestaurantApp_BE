using FoodRestaurantApp_BE.DbContexts;
using FoodRestaurantApp_BE.Models.Databases;
using FoodRestaurantApp_BE.Models.DTOs;

namespace FoodRestaurantApp_BE.Repositories
{
    public class FoodRepository(FoodRestaurantDbContext dbContext) : Repository<Food>(dbContext), 
                                                                     IFoodRepository
    {
        public IQueryable<Food> FindById(int id)
        {
            return _dbContext.Foods.Where(x => x.Id == id);
        }

        public IQueryable<Food> FindBySlug(string slug)
        {
            return _dbContext.Foods.Where(x => x.Slug == slug);
        }

        public IQueryable<Food> FindRelatedFoods(string slug)
        {
            throw new NotImplementedException();
        }

        public IQueryable<FoodBestSeller> FindBestSellingFoods(int top)
        {
            var sellingFoodRanking = _dbContext.SystemOrderLines.GroupBy(x => x.FoodId)
                                                                .Select(x => new { 
                                                                    Id = x.Key, 
                                                                    NumberOfSales = x.Sum(x => x.Quantity) 
                                                                })
                                                                .OrderByDescending(x => x.NumberOfSales);

            var bestSellingFoods = _dbContext.Foods.Join(sellingFoodRanking,
                                                         food => food.Id, topSeller => topSeller.Id,
                                                         (food, topSeller) => new FoodBestSeller()
                                                         {
                                                             Name = food.Name,
                                                             Price = food.Price,
                                                             Description = food.Description,
                                                             Image = food.Image,
                                                             MaxToppings = food.MaxToppings,
                                                             NumberOfSales = topSeller.NumberOfSales
                                                         })
                                                   .Take(top); 
            return bestSellingFoods;
        }
    }
}
