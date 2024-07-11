using System.Net.Http.Headers;

namespace FoodRestaurantApp_BE.Helpers
{
    public static class HttpContextExtensions
    {
        public static string? GetBearerToken(this HttpContext httpContext) 
        {
            var authorization = httpContext.Request.Headers.Authorization;

            if(AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
            {
                return headerValue.Parameter;    
            }
            return null;
        }
    }
}
