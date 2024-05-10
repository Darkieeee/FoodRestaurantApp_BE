namespace FoodRestaurantApp_BE.Models.Dto
{
    public class RoleDto(string roleId, string name) {
        public string Id { get; set; } = roleId;
        public string Name { get; set; } = name;
    }
}
