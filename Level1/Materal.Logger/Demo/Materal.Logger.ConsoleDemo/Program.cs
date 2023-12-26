using Materal.Logger.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Materal.Logger.ConsoleDemo
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            HostApplicationBuilder hostApplicationBuilder = Host.CreateApplicationBuilder(args);
            hostApplicationBuilder.AddMateralLogger(config =>
            {
                //config.AddConsoleTarget("ConsoleLog")
                //.AddAllTargetsRule(minLevel: LogLevel.Trace);
            });
            IHost host = hostApplicationBuilder.Build();
            host.UseMateralLogger();
            ILogger logger = host.Services.GetRequiredService<ILogger<Program>>();
            logger.LogTrace("Trace");
            logger.LogDebug("Debug");
            logger.LogInformation("Information");
            logger.LogWarning("Warning");
            logger.LogError("Error");
            logger.LogCritical("Critical");
            //ThreadPool.QueueUserWorkItem(_ =>
            //{
            //    using IDisposable? loggerScope = logger.BeginScope("CustomScope");
            //    while (true)
            //    {
            //        logger.LogTrace("Trace");
            //        logger.LogDebug("Debug");
            //        logger.LogInformation("Information");
            //        logger.LogWarning("Warning");
            //        logger.LogError("Error");
            //        logger.LogCritical("Critical");
            //        Thread.Sleep(1000);
            //    }
            //});
            await host.RunAsync();
        }
    }
}
