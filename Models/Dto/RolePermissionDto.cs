namespace FoodRestaurantApp_BE.Models.Dto
{
    public class RolePermissionDto(string roleId, string permissionId)
    {
        public string RoleId { get; set; } = roleId;
        public string PermissionId { get; set;} = permissionId;
    }
}
