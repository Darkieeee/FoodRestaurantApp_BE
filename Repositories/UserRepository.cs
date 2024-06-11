using FoodRestaurantApp_BE.DbContexts;
using FoodRestaurantApp_BE.Models.Databases;
using Microsoft.EntityFrameworkCore;

namespace FoodRestaurantApp_BE.Repositories {
	public class UserRepository(FoodRestaurantDbContext dbContext): IUserRepository
	{
		private readonly FoodRestaurantDbContext _dbContext = dbContext;

		public List<SystemUser> GetAll() {
			return _dbContext.Users.ToList();
		}

		public SystemUser? FindById(int id) {
			return _dbContext.Users.First(x => x.Id.Equals(id));
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

		public SystemUser? FindByNameAndPassword(string username, string password)
		{
			IQueryable<SystemUser> users = _dbContext.Users.Where(x => x.Name.Equals(username) 
																	&& x.Password.Equals(password))
														   .Include(x => x.Role);
			return users.FirstOrDefault();
		}

		public List<SystemUser> FindByName(string username)
		{
			IQueryable<SystemUser> users = _dbContext.Users.Where(x => x.Name.Equals(username))
														   .Include(x => x.Role);
			return users.ToList();
		}

        public async Task<int> UpdateAsync(SystemUser t)
        {
            _dbContext.Update(t);
            return await _dbContext.SaveChangesAsync();
        }

		public SystemUser? FindUserByName(string name)
		{
            IQueryable<SystemUser> users = _dbContext.Users.Where(x => x.Name.Equals(name))
														    .Include(x => x.Role);
            return users.FirstOrDefault();
        }
    }
}
