using FoodRestaurantApp_BE.Models.Databases;
using FoodRestaurantApp_BE.Models.DTOs;

namespace FoodRestaurantApp_BE.Services.Abstracts
{
    public interface IUserService
    {
        Pagination<UserDetailModelResponse> GetPagination(string? search, PageSizeOption sizeOption, int currentPage);
        List<UserShortDetailModelResponse> GetAll();
        [Obsolete("The method is considered less secure for user data information. Use GetByUuid() instead")]
        UserDetailModelResponse? GetById(int id);
        UserDetailModelResponse? GetByUuid(string uuid);
        bool Create(SystemUser user);
        bool Update(SystemUser user);
        bool CheckUsernameIfExists(string username);
        bool CheckEmailIfExists(string email);
        Task<SystemUser?> Authenticate(string username, string password);
        Task<bool> UpdateAsync(SystemUser user);
        Task<bool> CreateAsync(SystemUser user);
    }
}
