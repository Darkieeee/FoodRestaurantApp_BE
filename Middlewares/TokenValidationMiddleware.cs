using FoodRestaurantApp_BE.Filters;
using FoodRestaurantApp_BE.Helpers;
using FoodRestaurantApp_BE.Services.Abstracts;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.IdentityModel.Tokens;

namespace FoodRestaurantApp_BE.Middlewares
{
    public class TokenValidationMiddleware(RequestDelegate next, ITokenBlacklistService tokenBlacklistService)
    {
        private readonly RequestDelegate _next = next;
        private readonly ITokenBlacklistService _tokenBlacklistService = tokenBlacklistService;

        public async Task InvokeAsync(HttpContext context)
        {
            string token = context.GetBearerToken() ?? "";
            var endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;
            var attribute = endpoint?.Metadata.GetMetadata<ValidTokenAttribute>();

            if(attribute == null)
            {
                await _next(context);
                return;
            }

            if(token.IsNullOrEmpty())
            {
                throw new UnauthorizedAccessException("Missing bearer token");
            }

            bool rejected = await _tokenBlacklistService.IsTokenBlacklistAsync(token);
            if(rejected)
            {
                throw new UnauthorizedAccessException("Token rejected");
            }
            await _next(context);
        }
    }
}
