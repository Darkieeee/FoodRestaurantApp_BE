namespace FoodRestaurantApp_BE.Models.Databases
{
    public class RolePermission
    {
        public int RoleId { get; set; }
        public string PermissionId { get; set; } = string.Empty;
        public virtual Role Role { get; set; } = null!;
        public virtual Permission Permission { get; set; } = null!;
    }
}
