using System.Text.Json.Serialization;

namespace FoodRestaurantApp_BE.Models.Dto {
    public class UserDto(int id, string name, string password, string email, string roleId,
                         DateTime createdDate, bool isActive, DateTime? lastLogin = null) {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string Email { get; set; } = email;
        [JsonIgnore]
        public string Password { get; set; } = password;
        public DateTime CreatedDate { get; set; } = createdDate;
        public bool IsActive { get; set; } = isActive;
        public string RoleId { get; set; } = roleId;
        public DateTime? LastLogin { get; set; } = lastLogin;
    }
}
