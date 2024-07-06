namespace FoodRestaurantApp_BE.Models.DTOs
{
    public class AuthDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int? UserId { get; set; }
        public string? UserName { get; set; }
        public string? RoleName { get; set; }
        public bool? IsAdmin { get; set; }
        public string? Token { get; set; }
    }
}
