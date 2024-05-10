namespace FoodRestaurantApp_BE.Models.Dto
{
    public class PermissionDto(string permissionId, string name)
    {
        public string Id { get; set; } = permissionId;
        public string Name { get; set; } = name;
    }
}
