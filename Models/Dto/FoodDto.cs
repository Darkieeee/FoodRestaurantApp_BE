namespace FoodRestaurantApp_BE.Models.Dto
{
    public class FoodDto(int id, string name, string description, int price, 
                         int maxToppings, string slug, int type) {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string Description { get; set; } = description;
        public int Price {  get; set; } = price;
        public int MaxToppings { get; set; } = maxToppings;
        public string Slug { get; set; } = slug;
        public int Type { get; set; } = type;
    }
}
