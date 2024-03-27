using Materal.Logger.BusLogger;
using Materal.Logger.Extensions;
using Materal.Utils;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Materal.Logger.ConsoleHostDemo
{
    public class WriteLogService(ILogger<WriteLogService> logger) : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            ConsoleQueue.WriteLine("WriteLogService Start.");
            logger.SubscribeLogger(WriteLog);
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
            logger.UnsubscribeLogger(WriteLog);
            await Task.CompletedTask;
        }
        private static void WriteLog(BusLoggerWriterModel[] models)
        {
            foreach (BusLoggerWriterModel loggerModel in models)
            {
                ConsoleQueue.WriteLine($"[{loggerModel.ID}_{loggerModel.LogLevel}]:{loggerModel.Message}", ConsoleColor.DarkMagenta);
            }
        }
    }
}
