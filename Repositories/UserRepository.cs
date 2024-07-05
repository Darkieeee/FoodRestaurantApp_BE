using FoodRestaurantApp_BE.DbContexts;
using FoodRestaurantApp_BE.Models.Databases;
using Microsoft.EntityFrameworkCore;

namespace FoodRestaurantApp_BE.Repositories {
	public class UserRepository(FoodRestaurantDbContext dbContext): IUserRepository
	{
		private readonly FoodRestaurantDbContext _dbContext = dbContext;

		public IQueryable<SystemUser> GetAll() {
			return _dbContext.Users;
		}

		public IQueryable<SystemUser> FindById(int id) {
			return _dbContext.Users.Where(x => x.Id.Equals(id));
		}

		public int Insert(SystemUser u) {
			_dbContext.Users.Add(u);
			
			try {
				return _dbContext.SaveChanges();
			} catch {
				return -1;
			}
		}

		public int Update(SystemUser u)
		{
			return UpdateAsync(u).Result;
		}

		public int Delete(SystemUser t)
		{
			throw new NotImplementedException();
		}

		public IQueryable<SystemUser> FindByNameAndPassword(string username, string password)
		{
			IQueryable<SystemUser> user = _dbContext.Users.Where(x => x.Name.Equals(username) 
																   && x.Password.Equals(password));
			return user;
		}

		public IQueryable<SystemUser> FindByName(string username)
		{
			IQueryable<SystemUser> users = _dbContext.Users.Where(x => x.Name.Equals(username));
			return users;
        }

        public async Task<int> UpdateAsync(SystemUser t)
        {
            _dbContext.Users.Update(t);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> InsertAsync(SystemUser t)
        {
            _dbContext.Users.Add(t);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
