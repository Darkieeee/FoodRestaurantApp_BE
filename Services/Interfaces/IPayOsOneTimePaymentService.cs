using FoodRestaurantApp_BE.Models;

namespace FoodRestaurantApp_BE.Services.Interfaces
{
    public interface IPayOsOneTimePaymentService
    {
        void CreateOrder(string orderSysId);
        void TrackOrder(string orderSysId);
        void CancelOrder();
        byte[] GenerateSignature(PayOsOneTimePaymentRequest request);
    }
}
