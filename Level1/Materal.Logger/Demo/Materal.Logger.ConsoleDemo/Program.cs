using Materal.Logger.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Materal.Logger.ConsoleDemo
{
    public class Program
    {
        public static async void Main()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json", true, true);
            IConfiguration configuration = configurationBuilder.Build();
            IServiceCollection services = new ServiceCollection();
            services.AddLogging(builder =>
            {
                builder.AddMateralLogger(configuration, config =>
                {
                    config.AddConsoleTarget("CodeConsoleLog").AddAllTargetsRule(minLevel: LogLevel.Trace);
                });
                builder.AddMateralLoggerConfig(config =>
                {
                    config.AddConsoleTarget("Code2ConsoleLog").AddAllTargetsRule(minLevel: LogLevel.Trace);
                });
                //builder.AddMateralLogger(configuration);
            });
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            await serviceProvider.UseMateralLoggerAsync();
            ILogger logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogTrace("Trace");
        }
    }
}
