using Materal.Logger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MainDemo
{
    public class Program
    {
        public static void Main()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile("MateralLogger.json", optional: true, reloadOnChange: true)
                        .Build();
            LoggerConfig.Init(configuration);
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddMateralLogger();
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            ILogger<Program> logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Hello World!");
        }
    }
}