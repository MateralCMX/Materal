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
            LoggerManager.Init(configuration);
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddMateralLogger();
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            ILogger<Program> logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            Console.WriteLine("初始化完毕，按任意键开始测试");
            Console.ReadKey();
            for (int i = 0; i < 10000; i++)
            {
                logger.LogInformation($"Hello World!{i}");
            }
            Console.ReadKey();
        }
    }
}