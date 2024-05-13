using FoodRestaurantApp_BE.Models.Databases;

namespace FoodRestaurantApp_BE.Repositories
{
    public interface IUserRepository: IDisposable {
        List<SystemUser> GetAll();
        SystemUser FindById(string id);
        List<SystemUser> FindByName(string username);
        SystemUser? FindByNameAndPassword(string username, string password);
        bool Insert(SystemUser t);
        bool Update(SystemUser t);
        bool Delete(SystemUser t);
    }
}
