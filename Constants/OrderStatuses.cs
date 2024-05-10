namespace FoodRestaurantApp_BE.Constants
{
    class OrderStatuses
    {
        public static readonly short AWAITING_PAYMENT = 0;
        public static readonly short AWAITING_APPROVAL = 1;
        public static readonly short AWAITING_SHIPMENT = 2;
        public static readonly short SHIPPED = 3;
        public static readonly short CANCELLED = 4;
        public static readonly short REJECTED = 5;
    }
}