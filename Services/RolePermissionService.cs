using FoodRestaurantApp_BE.Models.DTOs;
using FoodRestaurantApp_BE.Repositories;
using FoodRestaurantApp_BE.Services.Abstracts;

namespace FoodRestaurantApp_BE.Services
{
    public class RolePermissionService(IPermissionRepository permissionRepository, IRolePermissionRepository rolePermissionRepository) : IRolePermissionService
    {
        private readonly IRolePermissionRepository _rolePermissionRepository = rolePermissionRepository;
        private readonly IPermissionRepository _permissionRepository = permissionRepository;

        public List<PermissionDto> GetPermissions(int roleId)
        {
            return _rolePermissionRepository.FindByRoleId(roleId)
                                            .Join(_permissionRepository.GetAll(),
                                                  role => role.PermissionId, permission => permission.Id,
                                                  (rolesPermissions, permission) => new PermissionDto() { Id = permission.Id, Name = permission.Name })
                                            .ToList();
        }
    }
}
