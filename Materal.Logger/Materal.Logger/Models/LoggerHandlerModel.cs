using Microsoft.Extensions.Logging;

namespace Materal.Logger.Models
{
    public class LoggerHandlerModel
    {
        public LogLevel LogLevel { get; set; }
        public Exception? Exception { get; set; }
        public string ThreadID { get; set; } = Environment.CurrentManagedThreadId.ToString();
        public string Message { get; set; } = string.Empty;
        public string? CategoryName { get; set; }
        public MateralLoggerScope? MateralLoggerScope { get; set; }
    }
}
