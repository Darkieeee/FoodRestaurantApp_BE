namespace FoodRestaurantApp_BE.Services.Abstracts
{
    public interface ITokenBlacklistService
    {
        Task<bool> IsTokenBlacklistAsync(string token);
    }
}
