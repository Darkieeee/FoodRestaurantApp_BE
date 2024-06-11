using FoodRestaurantApp_BE.Models.Databases;
using System.ComponentModel.DataAnnotations;


namespace FoodRestaurantApp_BE.Models.Requests
{
    public class SignUpRequest
    {
        [Required]
        public string name { get; set; }
        public string fullName { get; set; }
        public string email { get; set; } 

        public string password { get; set; }
    }
}
