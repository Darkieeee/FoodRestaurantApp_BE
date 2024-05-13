using FoodRestaurantApp_BE.Models;

namespace FoodRestaurantApp_BE.Services.Abstracts
{
    public interface IOrderSystemService
    { 
        void GetAll();
        string GetDetail(string orderSysId);
        void Delete(string orderSysId);
    }
}
