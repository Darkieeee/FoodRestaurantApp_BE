using FoodRestaurantApp_BE.Models.Securities;

namespace FoodRestaurantApp_BE.Services.Abstracts
{
    public interface IAuthService
    {
        AuthResult VerifyUser(string username, string password, bool isCustomer);
    }
}
