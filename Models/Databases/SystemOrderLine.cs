namespace FoodRestaurantApp_BE.Models.Databases {
    public class SystemOrderLine {
        public string SystemOrderId { get; set; } = string.Empty;
        public virtual SystemOrder SystemOrder { get; set; } = null!;
        public int FoodId { get; set; }
        public virtual Food Food { get; set; } = null!;
        public string Toppings { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public int BaseCost { get; set; }
        public int AdditionalCost { get; set; }
    }
}
