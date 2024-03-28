

using FoodRestaurantApp_BE.Contexts;
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

        UserDto IUserRepository.GetById(string id) {
            return _context.Users.First(x => x.Uid.Equals(id));
        }

        UserDto IUserRepository.Insert(UserDto t)
        {
            throw new NotImplementedException();
        }

        UserDto IUserRepository.Update(UserDto t)
        {
            throw new NotImplementedException();
        }

        bool IUserRepository.Delete(UserDto t)
        {
            throw new NotImplementedException();
        }

        int IUserRepository.Save() {
            return _context.SaveChanges();
        }

        void IDisposable.Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
