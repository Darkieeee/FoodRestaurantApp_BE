namespace FoodRestaurantApp_BE.Models {
    public class CreateOrderRequest(string CustomerName, string CustomerPhone, string CustomerAddress, 
                                    string? Description = "") {
        public string CustomerName { get; set; } = CustomerName;
        public string CustomerPhone { get; set; } = CustomerPhone;
        public string CustomerAddress { get; set; } = CustomerAddress;
        public string? Description { get; set; } = Description;
    }
    public class UpdateOrderRequest {

    }
}
