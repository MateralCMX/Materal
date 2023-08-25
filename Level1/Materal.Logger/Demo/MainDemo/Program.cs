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
            Random random = new();
            Console.ReadKey();
            for (int i = 1; i <= 10000; i++)
            {
                LogLevel logLevel = random.Next(0, 6) switch
                {
                    0 => LogLevel.Trace,
                    1 => LogLevel.Debug,
                    2 => LogLevel.Information,
                    3 => LogLevel.Warning,
                    4 => LogLevel.Error,
                    5 => LogLevel.Critical,
                    _ => throw new NotImplementedException()
                };
                logger.Log(logLevel, $"Hello World!{i}");
            }
            Console.WriteLine("输入完毕");
            Console.ReadKey();
        }
    }
}