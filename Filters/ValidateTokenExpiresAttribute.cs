using FoodRestaurantApp_BE.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;

namespace FoodRestaurantApp_BE.Filters
{
    public class ValidateTokenExpiresAttribute: TypeFilterAttribute
    {
        public ValidateTokenExpiresAttribute() : base(typeof(ValidateTokenExpiresFilterImpl))
        {
        }

        private class ValidateTokenExpiresFilterImpl(IDistributedCache cache) : IAsyncActionFilter
        {
            private readonly IDistributedCache _cache = cache;

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                string? token = context.HttpContext.GetBearerToken();

                if (token != null)
                {
                    if (!await _cache.GetRecordAsync<bool>(token))
                    {
                        context.Result = new UnauthorizedObjectResult("Token has expired");
                        return;
                    }
                }
                await next();
            }
        }
    }
}
