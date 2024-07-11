using FoodRestaurantApp_BE.Services.Abstracts;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace FoodRestaurantApp_BE.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CanAccessAttribute : Attribute
    {
        public required string[] Permissions { get; set; } = [];
    }
}
