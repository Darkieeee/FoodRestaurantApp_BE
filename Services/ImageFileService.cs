namespace FoodRestaurantApp_BE.Services
{
    public class ImageFileService : FileService
    {
        private readonly List<string> ImageExtensions = [
            ".JPG", ".JPEG", ".JPE", ".BMP", ".GIF", ".PNG"
        ];

        public override bool IsFileSupported(IFormFile file)
        {
            return !ImageExtensions.Contains(file.ContentType);
        }
    }
}
