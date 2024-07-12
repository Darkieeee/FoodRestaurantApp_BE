using System.Diagnostics.CodeAnalysis;

namespace FoodRestaurantApp_BE.Models.Logging
{
    public class FoodRestaurantFileLogger([NotNull] FoodRestaurantFileLoggerProvider provider) : ILogger
    {
        protected readonly FoodRestaurantFileLoggerProvider _provider = provider;

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if(!IsEnabled(logLevel))
            {
                return;
            }

            var fullfilePath = string.Format("{0}/{1}", _provider.Options.FolderPath,
                                                        _provider.Options.FilePath.Replace("{date}", DateTime.Now.ToString("yyyyMMdd")));
            var logRecord = string.Format("{0} [{1}] {2} {3}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                                               logLevel.ToString(),
                                                               formatter(state, exception),
                                                               exception?.StackTrace ?? "");

            using var streamWriter = new StreamWriter(fullfilePath, true);
            streamWriter.AutoFlush = true;
            streamWriter.NewLine = "\r\n";
            streamWriter.WriteLine(logRecord);
        }
    }
}
