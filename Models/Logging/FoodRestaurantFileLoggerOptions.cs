namespace FoodRestaurantApp_BE.Models.Logging
{
    public class FoodRestaurantFileLoggerOptions
    {
        public virtual required string FilePath { get; set; }
        public virtual required string FolderPath { get; set; }
    }
}
