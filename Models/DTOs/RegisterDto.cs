namespace FoodRestaurantApp_BE.Models.DTOs
{
    public class RegisterDto
    {
        public bool Success { get; set; }
        public int Code { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}