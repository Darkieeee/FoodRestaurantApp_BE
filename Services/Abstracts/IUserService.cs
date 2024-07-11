using FoodRestaurantApp_BE.Models.Databases;
using FoodRestaurantApp_BE.Models.DTOs;

namespace FoodRestaurantApp_BE.Services.Abstracts
{
    public interface IUserService
    {
        Pagination<UserShortDetails> GetPagination(string? search, PageSizeOption sizeOption, int currentPage);
        List<UserListView> GetAll();
        [Obsolete("The method is considered less secure for user data information. Use GetByUuid() instead")]
        UserDetails GetById(int id);
        UserDetails GetByUuid(string uuid);
        OperationResult<SystemUser> Create(SystemUser user);
        OperationResult<SystemUser> Update(SystemUser user);
        bool CheckUsernameIfExists(string username);
        bool CheckEmailIfExists(string email);
        Task<UserShortDetails?> Authenticate(string username, string password, bool isAdmin);
        Task<OperationResult<SystemUser>> UpdateAsync(SystemUser user);
        Task<OperationResult<SystemUser>> CreateAsync(SystemUser user);
    }
}
