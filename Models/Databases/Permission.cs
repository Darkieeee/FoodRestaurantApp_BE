namespace FoodRestaurantApp_BE.Models.Databases
{
    public class Permission(string id, string name)
    {
        public string Id { get; set; } = id;
        public string Name { get; set; } = name;
        public ICollection<Role> Roles { get; set; } = new List<Role>();
    }
}
