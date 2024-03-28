using System.Data;

namespace FoodRestaurantApp_BE.Models.Dto {
    public class AdminDto(string Name, string UserId, string RoleId) {
        public string Name { get; set; } = Name;
        public string UserId { get; set; } = UserId;
        public string? CreatedBy { get; set; }
        public string RoleId { get; set; } = RoleId;
    }
}
