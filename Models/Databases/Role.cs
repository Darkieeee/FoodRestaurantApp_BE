namespace FoodRestaurantApp_BE.Models.Databases
{
    public class Role {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public bool Editable { get; set; }
        public ICollection<SystemUser> SystemUsers { get; } = new List<SystemUser>();
        public ICollection<Permission> Permissions { get; } = new List<Permission>();
    }
}
