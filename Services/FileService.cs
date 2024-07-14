using FoodRestaurantApp_BE.Services.Abstracts;
using Microsoft.IdentityModel.Tokens;

namespace FoodRestaurantApp_BE.Services
{
    public class FileService : IFileService
    {
        public bool CheckFileIfExists(string? filepath)
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

        public string? GetFileContent(string? filepath)
        {
            if(!filepath.IsNullOrEmpty() && CheckFileIfExists(filepath))
            {
                try
                {
                    byte[] content = File.ReadAllBytes(filepath!);
                    return Convert.ToBase64String(content);
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }

        public virtual bool IsFileSupported(IFormFile file)
        {
            return true;
        }

        public async Task<string> UploadFile(string directory, IFormFile file)
        {
            if(file == null || file.Length == 0)
            {
                throw new ArgumentException("File cannot be null or empty.");
            }
            
            if(!IsFileSupported(file))
            {
                throw new NotSupportedException($"File with extension {file.ContentType} is not supported");
            }

            if(!Directory.Exists(directory)) 
            { 
                Directory.CreateDirectory(directory);
            }

            string fileName = Path.GetRandomFileName() + Path.GetExtension(file.FileName);  
            string path = Path.Combine(directory, fileName);

            using Stream stream = File.Create(path);
            await file.CopyToAsync(stream);

            return path;
        }

        public async Task<List<string>> UploadFiles(string directory, IEnumerable<IFormFile> files)
        {
            ArgumentNullException.ThrowIfNull(files, nameof(files));
            try
            {
                List<string> result = [];
                foreach (IFormFile file in files)
                {
                    string filePath = await UploadFile(directory, file);
                    result.Add(filePath);
                }
                return result;
            }
            catch
            {
                if(Path.Exists(directory)) 
                {
                    var dir = new DirectoryInfo(directory);
                    dir.Delete(true);
                }
                throw;
            }
        }
    }
}
