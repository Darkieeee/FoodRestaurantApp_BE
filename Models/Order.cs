namespace FoodRestaurantApp_BE.Models {
    public class CreateOrderRequest(string customerName, string customerPhone, string customerAddress,
                                    string paymentMethod, string? description = "") {
        public string CustomerName { get; set; } = customerName;
        public string CustomerPhone { get; set; } = customerPhone;
        public string CustomerAddress { get; set; } = customerAddress;
        public string? Description { get; set; } = description;
        public string PaymentMethod { get; set; } = paymentMethod;
        
    }
    public class UpdateOrderRequest {

    }
}
