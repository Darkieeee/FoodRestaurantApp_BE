using FoodRestaurantApp_BE.Models;

namespace FoodRestaurantApp_BE.Services
{
    public interface IOrderSystemService
    {
        void CreateOrder(CreateOrderRequest request);
        void GetAll();
        string GetDetail(string orderSysId);
        void Update(string orderSysId, UpdateOrderRequest request);
        void Delete(string orderSysId);
    }
}
