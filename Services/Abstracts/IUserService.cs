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
        DbDMLStatementResult Create(SystemUser user);
        DbDMLStatementResult Update(SystemUser user);
        bool CheckUsernameIfExists(string username);
        bool CheckEmailIfExists(string email);
        Task<UserDetailModelResponse?> Authenticate(string username, string password);
        Task<DbDMLStatementResult> UpdateAsync(SystemUser user);
        Task<DbDMLStatementResult> CreateAsync(SystemUser user);
    }
}
