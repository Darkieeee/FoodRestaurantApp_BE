namespace FoodRestaurantApp_BE.Models.DTOs
{
    public class ErrorDto
    {
        public required string Title { get; set; }
        public required List<string> Messages { get; set; }
    }
}
