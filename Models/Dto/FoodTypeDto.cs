namespace FoodRestaurantApp_BE.Models.Dto
{
    public class FoodTypeDto(int id, string name, string slug) {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string Slug { get; set; } = slug;
    }
}
