using FoodRestaurantApp_BE.Controllers;
using FoodRestaurantApp_BE.Middlewares;

namespace FoodRestaurantApp_BE.Extensions
{
    public static class TokenValidationExtensions
    {
        public static void UseTokenValidation(this IApplicationBuilder app)
        {
            app.UseMiddleware<TokenValidationMiddleware>();
        }
    }
}
