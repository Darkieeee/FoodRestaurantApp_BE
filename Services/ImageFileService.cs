namespace FoodRestaurantApp_BE.Services
{
    public class ImageFileService : FileService
    {
        private readonly List<string> ImageExtensions = ["image/png", "image/jpeg", "image/gif", "image/pjpeg"];

        public override bool IsFileSupported(IFormFile file)
        {
            return ImageExtensions.Contains(file.ContentType);
        }
    }
}
