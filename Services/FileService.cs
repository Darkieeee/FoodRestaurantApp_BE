using FoodRestaurantApp_BE.Services.Abstracts;

namespace FoodRestaurantApp_BE.Services
{
    public class FileService : IFileService
    {
        public bool CheckFileIfExists(string filepath)
        {
            return File.Exists(filepath);
        }

        public void DeleteFile(string filepath)
        {
            if(CheckFileIfExists(filepath))
            {
                File.Delete(filepath);
            }
        }

        public void DownloadFile(string filepath)
        {
            throw new NotImplementedException();
        }

        public FileStream GetFileContent(string filepath)
        {
            throw new NotImplementedException();
        }

        public virtual bool IsFileSupported(IFormFile file)
        {
            return true;
        }

        public async Task<string> UploadFile(string directory, IFormFile file)
        {
            if(!IsFileSupported(file))
            {
                throw new NotSupportedException($"File with extension {file.ContentType} is not supported");
            }

            if(!Directory.Exists(directory)) 
            { 
                Directory.CreateDirectory(directory);
            }

            string newFileName = string.Format("{0}_{1}{2}", Path.GetFileNameWithoutExtension(file.FileName) ?? "", 
                                                             DateTime.Now.ToString("ddMMyyyyHHmmss"), 
                                                             Path.GetExtension(file.FileName) ?? "");
            string path = Path.Combine(directory, newFileName);
            using Stream stream = File.Create(path);
            await file.CopyToAsync(stream);

            return path;
        }

        public void UploadFiles(string directory, IEnumerable<IFormFile> file)
        {
            throw new NotImplementedException();
        }
    }
}
