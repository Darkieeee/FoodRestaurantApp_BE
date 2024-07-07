using FoodRestaurantApp_BE.Models.Databases;

namespace FoodRestaurantApp_BE.Repositories
{
    public interface IUserRepository: IRepository<SystemUser>
    {
        IQueryable<SystemUser> FindById(int id);
        IQueryable<SystemUser> FindByUuid(string uuid);
        IQueryable<SystemUser> FindByName(string username);
        IQueryable<SystemUser> FindByEmail(string email);
    }
}
