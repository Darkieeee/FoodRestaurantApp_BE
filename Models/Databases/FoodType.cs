namespace FoodRestaurantApp_BE.Models.Databases
{
    public class FoodType {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public ICollection<Food> Foods { get; set; } = new List<Food>();
    }
}
