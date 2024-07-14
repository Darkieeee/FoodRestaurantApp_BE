using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace FoodRestaurantApp_BE.Models.DTOs
{
    public class FoodListView
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public required int Price { get; set; }
        public required string Url { get; set; }
        [JsonPropertyName("max_toppings")]
        public int MaxToppings { get; set; }
        [JsonPropertyName("img")]
        public string? Image { get; set; }
    }

    public class FoodDetails
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public required int Price { get; set; }
        public required string Url { get; set; }
        [JsonPropertyName("max_toppings")]
        public required int MaxToppings { get; set; }
        [JsonPropertyName("img")]
        public string? Image { get; set; }
        [JsonPropertyName("food_type")]
        public FoodTypeListView FoodType { get; set; } = null!;
    }

    public class FoodBestSeller
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public required int Price { get; set; }
        public required string Url { get; set; }
        [JsonPropertyName("max_toppings")]
        public required int MaxToppings { get; set; }
        [JsonPropertyName("img")]
        public string? Image { get; set; }
        [JsonPropertyName("food_type")]
        public FoodTypeListView FoodType { get; set; } = null!;
        public int NumberOfSales { get; set; }
    }

    public class StoreUpdateFoodRequest
    {
        [FromForm(Name = "name")]
        public required string Name { get; set; }
        [FromForm(Name = "description")]
        public string Description { get; set; } = string.Empty;
        [FromForm(Name = "price")]
        public required int Price { get; set; }
        [FromForm(Name = "max_toppings")]
        public int MaxToppings { get; set; }
        [FromForm(Name = "type")]
        public required int FoodType { get; set; }
        [FromForm(Name = "img")]
        public IFormFile? Image { get; set; }
    }
}
