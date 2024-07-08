namespace FoodRestaurantApp_BE.Services.Abstracts
{
    public interface IFileService
    {
        Task<string> UploadFile(string directory, IFormFile file);
        void UploadFiles(string directory, IEnumerable<IFormFile> file);
        void DownloadFile(string filepath);
        FileStream GetFileContent(string filepath);
        bool CheckFileIfExists(string filepath);
        void DeleteFile(string filepath);
    }
}
