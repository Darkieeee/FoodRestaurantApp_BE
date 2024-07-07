namespace FoodRestaurantApp_BE.Services.Abstracts
{
    public interface IFileService
    {
        void Upload(string filepath);
        void Download(string filepath);
        FileStream GetFileContent(string filepath);
    }
}
