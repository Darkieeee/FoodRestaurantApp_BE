using FoodRestaurantApp_BE.Models.DTOs;

namespace FoodRestaurantApp_BE.Services.Abstracts
{
    public interface IRolePermissionService
    {
        List<PermissionDto> GetPermissions(int role);
        List<PermissionDto> GetPermissions(string roleName);
    }
}
