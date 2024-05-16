using FoodRestaurantApp_BE.Models.Databases;

namespace FoodRestaurantApp_BE.Repositories
{
    public interface IRepository<T> where T: class
    {
        List<T> GetAll();
        bool Insert(T t);
        bool Update(T t);
        bool Delete(T t);
    }
}
