using System.Collections.Generic;

namespace FoodRestaurantApp_BE.Repositories
{
    public interface IRepository<T> where T: class
    {
        IQueryable<T> GetAll();
        int Insert(T t);
        int Update(T t);
        int Delete(T t);
        Task<int> InsertAsync(T t);
        Task<int> UpdateAsync(T t);
        Task<int> DeleteAsync(T t);
        Task<int> InsertRangeAsync(IEnumerable<T> t);
        Task<int> UpdateRangeAsync(IEnumerable<T> t);
        Task<int> DeleteRangeAsync(IEnumerable<T> t);
    }
}
