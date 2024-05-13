namespace FoodRestaurantApp_BE.Models.Databases {
    public class SystemOrder {
        public string Id { get; set; } = string.Empty;
        public string RecipientName { get; set; } = string.Empty;
        public string RecipientAddress { get; set; } = string.Empty;
        public string RecipientPhoneNumber { get; set; } = string.Empty;
        public int PlacedBy { get; set; }
        public int TotalPrice { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public short Status { get; set; }
        public int? ApprovedBy { get; set; }
        public string CancelReason { get; set; } = string.Empty;
        public bool Paid { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<Food> Foods { get; set; } = new List<Food>();
        public virtual SystemUser UserPlaced { get; set; } = null!;
        public virtual SystemUser? UserApproved { get; set; } = null;
    }
}
