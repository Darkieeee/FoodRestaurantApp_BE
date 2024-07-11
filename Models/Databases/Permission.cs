namespace FoodRestaurantApp_BE.Models.Databases
{
    public class Permission
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public ICollection<Role> Roles { get; set; } = new List<Role>();
    }
}
