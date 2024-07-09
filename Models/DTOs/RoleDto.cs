using FoodRestaurantApp_BE.Models.Databases;
using System.Text.Json.Serialization;

namespace FoodRestaurantApp_BE.Models.DTOs
{
    public class RoleListView
    {
        public required string Name { get; set; } 
        public required string Description { get; set; }
    }
    
    public class RoleDetails
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required bool Editable { get; set; }
        public required List<PermissionDto> Permissions { get; set; }

    }
}
