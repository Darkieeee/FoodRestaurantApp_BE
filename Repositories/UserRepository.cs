using FoodRestaurantApp_BE.DbContexts;
using FoodRestaurantApp_BE.Models.Databases;

namespace FoodRestaurantApp_BE.Repositories {
	public class UserRepository(FoodRestaurantDbContext dbContext): Repository<SystemUser>(dbContext), 
                                                                    IUserRepository
	{
		public IQueryable<SystemUser> FindByName(string username)
		{
			IQueryable<SystemUser> users = _dbContext.Users.Where(x => x.Name.Equals(username));
			return users;
        }

        public IQueryable<SystemUser> FindById(int id)
        {
            return _dbContext.Users.Where(x => x.Id.Equals(id));
        }

        public IQueryable<SystemUser> FindByEmail(string email)
        {
            IQueryable<SystemUser> users = _dbContext.Users.Where(x => x.Email.Equals(email));
            return users;
        }
    }
}
