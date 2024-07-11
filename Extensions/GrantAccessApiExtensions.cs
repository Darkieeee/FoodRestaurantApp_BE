using FoodRestaurantApp_BE.Middlewares;

namespace FoodRestaurantApp_BE.Extensions
{
    public static class GrantAccessApiExtensions
    {
        public static void UseGrantAccessApi(this IApplicationBuilder app)
        {
            app.UseMiddleware<GrantAccessApiMiddleware>();
        }
    }
}
