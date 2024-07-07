namespace FoodRestaurantApp_BE.Services.Abstracts
{
    public interface ITokenBlacklistService
    {
        Task AddToken(string token);
        Task<bool> IsTokenBlacklistAsync(string token);
    }
}
