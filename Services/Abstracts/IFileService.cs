namespace FoodRestaurantApp_BE.Services.Abstracts
{
    public interface IFileService
    {
        Task<string> UploadFile(string directory, IFormFile file);
        Task<List<string>> UploadFiles (string directory, IEnumerable<IFormFile> files);
        /// <summary>
        /// An encoded base64 string will be returned
        /// </summary>
        /// <param name="filepath">The path of the file</param>
        /// <returns></returns>
        string? GetFileContent(string? filepath);
        bool CheckFileIfExists(string? filepath);
        void DeleteFile(string filepath);
    }
}
