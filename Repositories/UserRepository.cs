using FoodRestaurantApp_BE.DbContexts;
using FoodRestaurantApp_BE.Models.Databases;
using Microsoft.EntityFrameworkCore;

namespace FoodRestaurantApp_BE.Repositories {
	public class UserRepository(FoodRestaurantDbContext dbContext): IUserRepository
	{
		private bool disposed = false;
		private readonly FoodRestaurantDbContext _dbContext = dbContext;

		protected virtual void Dispose(bool disposing) {
			if(!disposed && disposing) {
				_dbContext.Dispose();
			}
			disposed = true;
		}

		public List<SystemUser> GetAll() {
			return _dbContext.Users.ToList();
		}

		public SystemUser? FindById(int id) {
			return _dbContext.Users.First(x => x.Id.Equals(id));
		}

		public bool Insert(SystemUser u) {
			_dbContext.Users.Add(u);
			
			try {
				return _dbContext.SaveChanges() > 0;
			} catch {
				return false;
			}
		}

		public bool Update(SystemUser u)
		{
			throw new NotImplementedException();
		}

		public bool Delete(SystemUser t)
		{
			throw new NotImplementedException();
		}

		void IDisposable.Dispose() {
			Dispose(true);
			GC.SuppressFinalize(this);
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
	}
}
