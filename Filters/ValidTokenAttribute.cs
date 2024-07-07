using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace FoodRestaurantApp_BE.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class ValidTokenAttribute : Attribute, IAuthorizationFilter {
        public string[]? AllowedRoles { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            IEnumerable<Claim> claims = context.HttpContext.User.Claims;
            Claim? role = claims.FirstOrDefault(x => x.Type == ClaimTypes.Role) ?? throw new UnauthorizedAccessException("Invalid role provided");
            string[] roles = AllowedRoles ?? [];

            if (roles.Length > 0 && !roles.Contains(role.Value))
            {
                throw new UnauthorizedAccessException("You are not authorized to access this action");
            }
        }
    }
}
