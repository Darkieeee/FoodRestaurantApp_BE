namespace FoodRestaurantApp_BE.Models.DTOs
{
    public class OperationResult<T> where T: class
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Value { get; set; }
        public Exception? Exception { get; set; } 
    }
}
