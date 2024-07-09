using FoodRestaurantApp_BE.Constants;
using FoodRestaurantApp_BE.Models.Databases;
using FoodRestaurantApp_BE.Models.DTOs;
using FoodRestaurantApp_BE.Repositories;
using FoodRestaurantApp_BE.Services.Abstracts;

namespace FoodRestaurantApp_BE.Services
{
    public class RoleService(IRoleRepository repository, IPermissionRepository permissionRepository,
                             IRolePermissionRepository rolePermissionRepository) : IRoleService
    {
        private readonly IRoleRepository _repository = repository;
        private readonly IPermissionRepository _permissionRepository = permissionRepository;
        private readonly IRolePermissionRepository _rolePermissionRepository = rolePermissionRepository;

        public Role? GetByName(string name)
        {
            return _repository.FindByName(name).FirstOrDefault();
        }

        private RoleDetails GetDetails(Role role)
        {
            List<string> permissionIds = _rolePermissionRepository.FindByRoleId(role.Id)
                                                                  .Select(x => x.PermissionId)
                                                                  .ToList();
;
            List<PermissionDto> permissions = _permissionRepository.FindByIds(permissionIds)
                                                                   .Select(x => new PermissionDto() { Id = x.Id, Name = x.Name })
                                                                   .ToList();
            return new RoleDetails() {
                Name = role.Name,
                Description = role.Description,
                Editable = true,
                Permissions = permissions
            };
        }
    }
}
