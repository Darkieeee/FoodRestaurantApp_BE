using FoodRestaurantApp_BE.DbContexts;
using FoodRestaurantApp_BE.Models.Databases;
using Microsoft.EntityFrameworkCore;

namespace FoodRestaurantApp_BE.Repositories {
    public class UserRepository(FoodRestaurantDbContext context) : IUserRepository
    {
        private bool disposed = false;
        private readonly FoodRestaurantDbContext _context = context;

        protected virtual void Dispose(bool disposing) {
            if(!disposed && disposing) {
                _context.Dispose();
            }
            disposed = true;
        }

        List<SystemUser> IUserRepository.GetAll() {
            return _context.Users.ToList();
        }

        SystemUser IUserRepository.FindById(string id) {
            return _context.Users.First(x => x.Id.Equals(id));
        }

        bool IUserRepository.Insert(SystemUser u) {
            _context.Users.Add(u);
            
            try {
                return _context.SaveChanges() > 0;
            } catch {
                return false;
            }
        }

        bool IUserRepository.Update(SystemUser u)
        {
            throw new NotImplementedException();
        }

        bool IUserRepository.Delete(SystemUser t)
        {
            throw new NotImplementedException();
        }

        void IDisposable.Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public SystemUser? FindByNameAndPassword(string username, string password)
        {
            IQueryable<SystemUser> users = _context.Users.Where(x => x.Name.Equals(username) && x.Password.Equals(password));
            return users.FirstOrDefault();
        }

        public List<SystemUser> FindByName(string username)
        {
            IQueryable<SystemUser> users = _context.Users.Where(x => x.Name.Equals(username)).Include(x => x.Role);
            return users.ToList();
        }
    }
}
