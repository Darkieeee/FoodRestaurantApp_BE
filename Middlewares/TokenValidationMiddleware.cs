using FoodRestaurantApp_BE.Filters;
using FoodRestaurantApp_BE.Helpers;
using FoodRestaurantApp_BE.Services.Abstracts;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace FoodRestaurantApp_BE.Middlewares
{
    public class TokenValidationMiddleware(RequestDelegate next, ITokenBlacklistService tokenBlacklistService,
                                           IConfiguration configuration)
    {
        private readonly RequestDelegate _next = next;
        private readonly ITokenBlacklistService _tokenBlacklistService = tokenBlacklistService;
        private readonly IConfiguration _configuration = configuration;

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

            var isToken = await IsValidTokenParameter(token);
            
            if(!isToken)
            {
                throw new UnauthorizedAccessException("Invalid authorization token provided");
            }    

            bool rejected = await _tokenBlacklistService.IsTokenBlacklistAsync(token);
            
            if(rejected)
            {
                throw new UnauthorizedAccessException("Your session token has expired! Please authorize to get this access");
            }
            
            await _next(context);
        }

        private async Task<bool> IsValidTokenParameter(string token)
        {
            var jsonTokenHandler = new JwtSecurityTokenHandler();
            var secretKey = _configuration["JwtBearer:SecretKey"];
            var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey!);

            var validationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes)
            };

            var validationResult =  await jsonTokenHandler.ValidateTokenAsync(token, validationParameters);
            return validationResult.IsValid;
        }
    }
}
