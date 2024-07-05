using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FoodRestaurantApp_BE.Models.Logging
{
    [ProviderAlias("FoodRestaurantFile")]
    public class FoodRestaurantFileLoggerProvider : ILoggerProvider
    {
        public readonly FoodRestaurantFileLoggerOptions Options;

        public FoodRestaurantFileLoggerProvider(IOptions<FoodRestaurantFileLoggerOptions> Options)
        {
            this.Options = Options.Value;

            if(this.Options.FolderPath.IsNullOrEmpty())
            {
                this.Options.FolderPath = Environment.CurrentDirectory + "\\Logs";
            }
            
            if(!Directory.Exists(this.Options.FolderPath))
            {
                Directory.CreateDirectory(this.Options.FolderPath);
            }

        }

        public ILogger CreateLogger(string categoryName)
        {
            return new FoodRestaurantFileLogger(this);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
