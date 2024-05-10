namespace FoodRestaurantApp_BE.Models.Dto {
    public class SystemOrderDto(string id, string shipToAddress, string recipientName,
                                string recipientPhoneNumber, string placedBy, int totalPrice,
                                string paymentMethod, string note, short status, string approvedBy,
                                string cancelReason, bool paid, DateTime createdDate) {
        public string Id { get; set; } = id;
        public string RecipientName { get; set; } = recipientName;
        public string RecipientAddress { get; set; } = shipToAddress;
        public string RecipientPhoneNumber { get; set; } = recipientPhoneNumber;
        public string PlacedBy { get; set; } = placedBy;
        public int TotalPrice
        {
            get => totalPrice;
            set {
                if(totalPrice < 0) {
                    throw new ArgumentException(nameof(totalPrice), "Total price must be positive");
                }
                totalPrice = value;
            }
        }
        public string PaymentMethod { get; set; } = paymentMethod;
        public string Note { get; set; } = note;
        public short Status { get; set; } = status;
        public string ApprovedBy { get; set; } = approvedBy;
        public string CancelReason { get; set; } = cancelReason;
        public bool Paid { get; set; } = paid;
        public DateTime CreatedDate { get; set; } = createdDate;
    }
}
