namespace FoodRestaurantApp_BE.Models.DTOs
{
    public class StatusResponse
    {
        public bool Success { get; set; }
        public List<string> Messages { get; set; } = [];
    }
}
