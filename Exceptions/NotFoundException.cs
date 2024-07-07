namespace FoodRestaurantApp_BE.Exceptions
{
    public class NotFoundException(string? message, Exception? innerException) : Exception(message, innerException)
    {
    }
}
