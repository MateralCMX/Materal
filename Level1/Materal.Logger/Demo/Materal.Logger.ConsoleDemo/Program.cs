using Materal.Logger.Extensions;
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
            services.AddLogging(builder =>
            {
                builder.AddMateralLogger(configuration);
            });
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            await serviceProvider.UseMateralLoggerAsync();
            ILogger logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogTrace("Trace");
        }
    }
}
