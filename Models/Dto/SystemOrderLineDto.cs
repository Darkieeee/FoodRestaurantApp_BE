namespace FoodRestaurantApp_BE.Models.Dto {
    public class SystemOrderLineDto(string sysOrderId, int foodId, string toppings, 
                                    int quantity, int baseCost, int additionalCost) {
        public string SysOrderId { get; set; } = sysOrderId;
        public int FoodId { get; set; } = foodId;
        public string Toppings { get; set; } = toppings;
        public int Quantity { get; set; } = quantity;
        public int BaseCost { get; set; } = baseCost;
        public int AdditionalCost { get; set; } = additionalCost;
    }
}
