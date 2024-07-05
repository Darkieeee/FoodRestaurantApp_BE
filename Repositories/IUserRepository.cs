using FoodRestaurantApp_BE.Models.Databases;

namespace FoodRestaurantApp_BE.Repositories
{
    public interface IUserRepository: IRepository<SystemUser>
    {
        IQueryable<SystemUser> FindById(int id);
        IQueryable<SystemUser> FindByName(string username);
        IQueryable<SystemUser> FindByNameAndPassword(string username, string password);
    }
}
