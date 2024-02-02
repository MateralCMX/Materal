using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RC.ConfigClient.Extensions;

namespace RC.ConfileClient.ConsoleDemo
{
    public class Program
    {
        public static async Task Main()
        {
            ConfigurationBuilder configurationBuilder = new();
            configurationBuilder.AddDefaultNameSpace("http://127.0.0.1:8700/RCESDEVAPI", "TestProject", 10);
            IConfiguration configuration = configurationBuilder.Build();
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddOptions();
            serviceCollection.Configure<DemoConfig>(configuration);
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            IOptionsMonitor<DemoConfig> config = serviceProvider.GetRequiredService<IOptionsMonitor<DemoConfig>>();
            while (true)
            {
                Console.WriteLine($"[{DateTime.Now:yyyy/MM/dd HH:mm:ss}]ApplicationName: {config.CurrentValue.ApplicationName}");
                await Task.Delay(1000);
            }
        }
    }
}
