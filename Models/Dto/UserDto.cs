namespace FoodRestaurantApp_BE.Models.Dto {
    public class UserDto(string uid, string name, string password, string email, 
                         DateTime createdDate, bool isActive, DateTime? lastLogin) {
        public string Uid { get; set; } = uid;
        public string Name { get; set; } = name;
        public string Email { get; set; } = email;
        public string Password { get; set; } = password;
        public DateTime CreatedDate { get; set; } = createdDate;
        public bool IsActive { get; set; } = isActive;
        public DateTime? LastLogin { get; set; } = lastLogin;
    }
}
