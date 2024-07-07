namespace FoodRestaurantApp_BE.Models.DTOs
{
    public class UserDetailModelResponse
    {
        public required string Uuid { get; set; }
        public required string Name { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required RoleModelResponse Role { get; set; }
    }

    public class UserShortDetailModelResponse
    {
        public required string Uuid { get; set; }
        public required string Name { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
    }

    public class CreateUserRequest
    {
        public required string Name { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required int RoleId { get; set; }
        public IFormFile? Avatar { get; set; } = null;
    }
}
