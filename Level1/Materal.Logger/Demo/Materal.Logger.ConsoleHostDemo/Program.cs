using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Materal.Logger.ConsoleHostDemo
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            HostApplicationBuilder applicationBuilder = Host.CreateApplicationBuilder(args);
            applicationBuilder.AddMateralLogger();
            //applicationBuilder.Logging.AddMateralLogger();
            //applicationBuilder.Services.AddLogging(builder => builder.AddMateralLogger());
            //applicationBuilder.Services.AddMateralLogger();
            IHost host = applicationBuilder.Build();
            await host.UseMateralLoggerAsync();
            ILogger logger = host.Services.GetRequiredService<ILogger<Program>>();
            logger.LogTrace("Trace");
            logger.LogDebug("Debug");
            logger.LogInformation("Information");
            logger.LogWarning("Warning");
            logger.LogError("Error");
            logger.LogCritical("Critical");
            await host.RunAsync();
        }
    }
}
