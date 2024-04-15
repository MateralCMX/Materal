using Materal.Logger.BusLogger;
using Materal.Utils;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Materal.Logger.ConsoleHostDemo
{
    public class WriteLogService(ILogger<WriteLogService> logger) : IHostedService, ILogMonitor
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            ConsoleQueue.WriteLine("WriteLogService Start.");
            logger.Subscribe(this);
            while (!cancellationToken.IsCancellationRequested)
            {
                logger.LogTrace("Trace");
                logger.LogDebug("Debug");
                logger.LogInformation("Information");
                logger.LogWarning("Warning");
                logger.LogError("Error");
                logger.LogCritical("Critical");
                await Task.Delay(1000);
            }
        }
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            ConsoleQueue.WriteLine("WriteLogService Stop.");
            logger.Unsubscribe(this);
            await Task.CompletedTask;
        }
        public async Task HandlerNewLogInfoAsync(LogModel logModel)
        {
            ConsoleQueue.WriteLine($"[{logModel.ID}_{logModel.LogLevel}]:{logModel.Message}", ConsoleColor.DarkMagenta);
            await Task.CompletedTask;
        }
    }
}
