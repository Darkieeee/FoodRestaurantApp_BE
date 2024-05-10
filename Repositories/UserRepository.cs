

using FoodRestaurantApp_BE.DbContexts;
using FoodRestaurantApp_BE.Models.Dto;

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

        List<UserDto> IUserRepository.GetAll() {
            return _context.Users.ToList();
        }

        UserDto IUserRepository.FindById(string id) {
            return _context.Users.First(x => x.Id.Equals(id));
        }

        bool IUserRepository.Insert(UserDto u) {
            _context.Users.Add(u);
            
            try {
                return _context.SaveChanges() > 0;
            } catch {
                return false;
            }
        }

        bool IUserRepository.Update(UserDto u)
        {
            throw new NotImplementedException();
        }

        bool IUserRepository.Delete(UserDto t)
        {
            throw new NotImplementedException();
        }

        void IDisposable.Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public UserDto? FindByNameAndPassword(string username, string password)
        {
            IQueryable<UserDto> users = _context.Users.Where(x => x.Name.Equals(username) && x.Password.Equals(password));
            return users.FirstOrDefault();
        }

        public List<UserDto> FindByName(string username)
        {
            IQueryable<UserDto> users = _context.Users.Where(x => x.Name.Equals(username) && x.Password.Equals(password));
            return users.ToList();
        }
    }
}
