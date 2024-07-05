using FoodRestaurantApp_BE.Helpers;
using FoodRestaurantApp_BE.Services.Abstracts;
using Microsoft.Extensions.Caching.Distributed;

namespace FoodRestaurantApp_BE.Services
{
    public class JwtTokenBlacklistService(IDistributedCache cache) : ITokenBlacklistService
    {
        private readonly IDistributedCache _cache = cache;

        public async Task<bool> IsTokenBlacklistAsync(string token)
        {
            return await _cache.GetRecordAsync<bool>(token);
        }
    }
}
