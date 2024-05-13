namespace FoodRestaurantApp_BE.Models.Securities
{
    public class AuthResult(bool success = true, string message = "", int? userId = null, 
                            string? userName = null, string? roleName = null, string? token = null)
    {
        public bool Success { get; set; } = success;
        public string Message { get; set; } = message;
        public int? UserId { get; set; } = userId;
        public string? UserName { get; set; } = userName;
        public string? RoleName { get; set; } = roleName;
        public string? Token { get; set; } = token;
    }
}
