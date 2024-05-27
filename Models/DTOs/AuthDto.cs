namespace FoodRestaurantApp_BE.Models.DTOs
{
    public class AuthDto
    {
        public static readonly short IS_VALID = 00;
        public static readonly short WRONG_USERNAME = 03;
        public static readonly short WRONG_PASSWORD = 05;

        public bool Success { get; set; }
        public int Code { get; set; }
        public string Message { get; set; } = string.Empty;
        public int? UserId { get; set; }
        public string? UserName { get; set; }
        public string? RoleName { get; set; }
        public string? Token { get; set; }
    }
}
