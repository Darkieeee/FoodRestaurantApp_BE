using FoodRestaurantApp_BE.Extensions;
using FoodRestaurantApp_BE.Filters;
using FoodRestaurantApp_BE.Services.Abstracts;
using Microsoft.AspNetCore.Http.Features;
using System.Security.Claims;

namespace FoodRestaurantApp_BE.Middlewares
{
    public class GrantAccessApiMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context, IRolePermissionService rolePermissionService)
        {
            var endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;
            var attribute = endpoint?.Metadata.GetMetadata<CanAccessAttribute>();

            if (attribute == null)
            {
                await _next(context);
                return;
            }

            if (context.User.Identity != null && context.User.Identity.IsAuthenticated)
            {
                IEnumerable<Claim> claims = context.User.Claims;
                Claim? role = claims.FirstOrDefault(x => x.Type == ClaimTypes.Role) ?? throw new UnauthorizedAccessException("Invalid role provided");

                List<string> permissions = rolePermissionService.GetPermissions(role.Value)
                                                                .Select(x => x.Id)
                                                                .ToList();

                if (attribute.Permissions.Contains(permissions))
                {
                    await _next(context);
                    return;
                }
                throw new UnauthorizedAccessException("You are not authorized to do this action");
            }
            throw new UnauthorizedAccessException("This request is unauthorized! Please authorize before doint this action");
        }
    }
}
