namespace FoodRestaurantApp_BE.Helpers
{
    public static class HttpContextExtensions
    {
        public static string GetBearerToken(this HttpContext httpContext) 
        {
            return httpContext.Request.Headers.Authorization[0]?.Split(" ")[1] ?? "";
        }
    }
}
