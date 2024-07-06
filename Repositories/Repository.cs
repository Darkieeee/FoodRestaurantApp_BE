
using FoodRestaurantApp_BE.DbContexts;

namespace FoodRestaurantApp_BE.Repositories
{
    public class Repository<T>(FoodRestaurantDbContext dbContext) : IRepository<T> where T: class
    {
        protected readonly FoodRestaurantDbContext _dbContext = dbContext;

        public async Task<int> InsertRangeAsync(IEnumerable<T> t)
        {
            await _dbContext.AddRangeAsync(t);
            return await _dbContext.SaveChangesAsync();
        }

        public int Delete(T t)
        {
            return DeleteAsync(t).Result;
        }

        public async Task<int> DeleteAsync(T t)
        {
            _dbContext.Remove(t);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteRangeAsync(IEnumerable<T> t)
        {
            _dbContext.RemoveRange(t);
            return await _dbContext.SaveChangesAsync();
        }

        public IQueryable<T> GetAll()
        {
            return _dbContext.Set<T>();
        }

        public int Insert(T t)
        {
            return InsertAsync(t).Result;
        }

        public async Task<int> InsertAsync(T t)
        {
            await _dbContext.AddAsync(t);
            return await _dbContext.SaveChangesAsync();
        }

        public int Update(T t)
        {
            return UpdateAsync(t).Result;
        }

        public async Task<int> UpdateAsync(T t)
        {
            _dbContext.Update(t);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateRangeAsync(IEnumerable<T> t)
        {
            _dbContext.UpdateRange(t);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
