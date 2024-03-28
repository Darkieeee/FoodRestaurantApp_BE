using FoodRestaurantApp_BE.Models;
using FoodRestaurantApp_BE.Services.Abstracts;
using FoodRestaurantApp_BE.Services.Interfaces;

namespace FoodRestaurantApp_BE.Services
{
    class PayOsOneTimePaymentService(IConfiguration configuration): PayOsService(configuration), IPayOsOneTimePaymentService {
        public override PayOsProvidedService GetPaymentType() {
            return PayOsProvidedService.OneTimePayment;
        }

        void IPayOsOneTimePaymentService.CreateOrder(string orderSysId) {
            throw new NotImplementedException();
        }

        void IPayOsOneTimePaymentService.TrackOrder(string orderSysId) {
            throw new NotImplementedException();
        }

        byte[] IPayOsOneTimePaymentService.GenerateSignature(PayOsOneTimePaymentRequest request) {
            string rawQuery = $"amount=${request.Amount}&cancelUrl=${request.CancelUrl}&description=${request.Description}&"
                            + $"orderCode=${request.OrderCode}&returnUrl=${request.ReturnUrl}";
            byte[] bytes = Convert.FromHexString(rawQuery);
            return Hmac!.ComputeHash(bytes);
        }

        public void CancelOrder() {
            throw new NotImplementedException();
        }
    }
}