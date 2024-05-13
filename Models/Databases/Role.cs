namespace FoodRestaurantApp_BE.Models.Databases
{
    public class Role(int id, string name, string description) {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string Description { get; set; } = description;
        public ICollection<SystemUser> SystemUsers { get; } = new List<SystemUser>();
        public ICollection<Permission> Permissions { get; } = new List<Permission>();
    }
}
