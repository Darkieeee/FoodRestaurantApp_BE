namespace FoodRestaurantApp_BE.Models.DTOs
{
    public class CreateFoodRequest
    {
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Price { get; set; }
        public int MaxToppings { get; set; }
    }
}
