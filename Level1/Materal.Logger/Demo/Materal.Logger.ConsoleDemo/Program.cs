using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Materal.Logger.ConsoleDemo
{
    public class Program
    {
        public static void Main()
        {
            IServiceProvider serviceProvider = new ServiceCollection()
                .AddLogging(builder =>
                {
                    builder.AddMateralLogger(configuration =>
                    {
                        configuration.MinLogLevel = LogLevel.Information;
                        configuration.MaxLogLevel = LogLevel.Critical;
                    });
                })
                .BuildServiceProvider();
            ILogger<Program> logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogTrace("Trace Log.");
            logger.LogDebug("Debug Log.");
            logger.LogInformation("Information Log.");
            logger.LogWarning("Warning Log.");
            logger.LogError("Error Log.");
            logger.LogTrace("Trace Log.");
        }
    }
}
