using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Materal.Logger.ConsoleDemo
{
    public class Program
    {
        public static async Task Main()
        {
            ConfigurationBuilder configurationBuilder = new();
            configurationBuilder.AddJsonFile("MateralLogger.json", true, true);
            IConfiguration configuration = configurationBuilder.Build();
            IServiceCollection services = new ServiceCollection();
            services.AddMateralLogger(configuration);
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            await serviceProvider.UseMateralLoggerAsync();
            ILogger logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            while (true)
            {
                Console.ReadLine();
                logger.LogTrace("Trace");
                //logger.LogDebug("Debug");
                //logger.LogInformation("Information");
                //logger.LogWarning("Warning");
                //logger.LogError("Error");
                //logger.LogCritical("Critical");
            }
        }
    }
}
