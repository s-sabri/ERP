using Serilog;
using Shared.Application.Logging.Interfaces;

namespace Shared.Infrastructure.Logging
{
    public class SerilogAdapter<T> : IAppLogger<T>
    {
        private static readonly ILogger _logger = Log.ForContext<T>();

        public void LogInformation(string message)
        {
            _logger.Information("{Message}", message);
        }
        public void LogWarning(string message)
        {
            _logger.Warning("{Message}", message);
        }
        public void LogError(string message, Exception? ex = null)
        {
            if (ex == null)
                _logger.Error("{Message}", message);
            else
                _logger.Error(ex, "{Message}", message);
        }
    }
}
