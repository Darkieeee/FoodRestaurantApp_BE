namespace FoodRestaurantApp_BE.Models.Dto {
    public class UserAddressDto(string RecipientName, string RecipientAddress, string RecipientPhone, string UserId) {
        public string RecipientName { get; set; } = RecipientName;
        public string RecipientAddress { get; set; } = RecipientAddress;
        public string RecipientPhone { get; set; } = RecipientPhone;
        public string UserId { get; set; } = UserId;
    }
}
