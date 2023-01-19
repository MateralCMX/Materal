using Microsoft.Extensions.Logging;

namespace Materal.Logger
{
    public class MateralLoggerProvider : ILoggerProvider, IDisposable
    {
        public ILogger CreateLogger(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                return new MateralLogger();
            }
            return new MateralLogger(categoryName);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
