namespace FoodRestaurantApp_BE.Models.Dto
{    
    public class ToppingDto(int id, string name, int price) {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
        public int Price { get; set; } = price;
    }
}
