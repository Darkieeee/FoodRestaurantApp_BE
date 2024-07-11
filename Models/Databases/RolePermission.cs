namespace FoodRestaurantApp_BE.Models.Databases
{
    public class RolePermission
    {
        public required int RoleId { get; set; }
        public required string PermissionId { get; set; }
        public virtual Role Role { get; set; } = null!;
        public virtual Permission Permission { get; set; } = null!;
    }
}
