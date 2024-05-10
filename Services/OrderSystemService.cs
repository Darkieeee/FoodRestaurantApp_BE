using FoodRestaurantApp_BE.Models;

namespace FoodRestaurantApp_BE.Services
{
    public class OrderSystemService : IOrderSystemService {
        public string GetDetail(string orderSysId) {
            throw new NotImplementedException();
        }

        void IOrderSystemService.CreateOrder(CreateOrderRequest request) {
            throw new NotImplementedException();
        }

        void IOrderSystemService.Delete(string orderSysId) {
            throw new NotImplementedException();
        }

        void IOrderSystemService.GetAll() {
            throw new NotImplementedException();
        }

        void IOrderSystemService.Update(string orderSysId, UpdateOrderRequest request) {
            throw new NotImplementedException();
        }
    }
}
