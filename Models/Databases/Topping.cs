namespace FoodRestaurantApp_BE.Models.Databases
{    
    public class Topping(int id, string name, int price) {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
        public int Price { get; set; } = price;
    }
}
