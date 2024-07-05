using FoodRestaurantApp_BE.Models.Logging;

namespace FoodRestaurantApp_BE.Helpers
{
    public static class FoodRestaurantFileLoggerExtensions
    {
        public static ILoggingBuilder AddFoodRestaurantFileLogger(this ILoggingBuilder builder, Action<FoodRestaurantFileLoggerOptions> configuration)
        {
            builder.Services.AddSingleton<ILoggerProvider, FoodRestaurantFileLoggerProvider>();
            builder.Services.Configure(configuration);

            return builder;
        }
    }
}
