namespace FoodRestaurantApp_BE.Models.Databases
{
    public class Food {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Price { get; set; }
        public int MaxToppings { get; set; }
        public required string Slug { get; set; }
        public int TypeId { get; set; }
        public string? Image { get; set; }
        public virtual FoodType Type { get; set; } = null!;
        public ICollection<SystemOrder> SystemOrders = new List<SystemOrder>();
    }
}
