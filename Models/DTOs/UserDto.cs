namespace FoodRestaurantApp_BE.Models.DTOs
{
    public class UserDetailModelResponse
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required RoleModelResponse Role { get; set; }
    }

    public class UserShortDetailModelResponse
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
    }
}
