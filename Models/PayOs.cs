namespace FoodRestaurantApp_BE.Models {
    public class PayOsOneTimePaymentRequest {
        /// <summary>
        /// Mã đơn hàng
        /// </summary>
        public int OrderCode { get; set; }
        /// <summary>
        /// Số tiền thanh toán
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// Mô tả thanh toán
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Tên của người mua hàng
        /// </summary>
        public string? BuyerName { get; set; }
        /// <summary>
        /// Email của người mua hàng
        /// </summary>
        public string? BuyerEmail { get; set; }
        /// <summary>
        /// Số điện thoại người mua hàng
        /// </summary>
        public string? BuyerPhone { get; set; }
        /// <summary>
        /// Địa chỉ của người mua hàng
        /// </summary>
        public string? BuyerAddress { get; set; }
        /// <summary>
        /// Danh sách các sản phẩm thanh toán
        /// </summary>
        public List<PayOsOrderItem>? Items { get; set; }
        /// <summary>
        /// URL nhận dữ liệu khi người dùng chọn Huỷ đơn hàng
        /// </summary>
        public string? CancelUrl { get; set; }
        /// <summary>
        /// URL nhận dữ liệu khi đơn hàng thanh toán thành công
        /// </summary>
        public string? ReturnUrl { get; set; }
        /// <summary>
        /// Thời gian hết hạn của link thanh toán
        /// </summary>
        public long ExpiredAt { get; set; }
        /// <summary>
        /// Chữ ký xác thực dữ liệu thanh toán
        /// </summary>
        public string? Signature { get; set; }
    }

    public class PayOsOrderItem {
        /// <summary>
        /// Tên sản phẩm
        /// </summary>
        public string? Name { get; set; }
        public int? Quantity { get; set; }
        public int? Price { get; set; }
    }
}
