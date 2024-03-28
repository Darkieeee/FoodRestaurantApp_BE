namespace FoodRestaurantApp_BE.Models.Dto
{
    public class FoodDto(int id, string name, string description, int price, 
                         string slug, int foodType) {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string Description { get; set; } = description;
        public int Price {  get; set; } = price;
        public string Slug { get; set; } = slug;
        public int FoodType { get; set; } = foodType;
    }
}
