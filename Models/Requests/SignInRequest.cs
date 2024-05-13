namespace FoodRestaurantApp_BE.Models.Requests
{
    public class SignInRequest
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
