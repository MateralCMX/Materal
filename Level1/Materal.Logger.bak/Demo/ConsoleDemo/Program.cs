using Materal.Logger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace ConsoleDemo
{
    public class Program
    {
        private readonly static IConfiguration _configuration;
        private readonly static IServiceCollection _serviceCollection;
        private readonly static IServiceProvider _serviceProvider;
        private readonly static ILogger<Program> _logger;
        static Program()
        {
            _configuration = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .Build();
            _serviceCollection = new ServiceCollection();
            _serviceCollection.AddSingleton(_configuration);
            _serviceCollection.AddMateralLogger();
            //_serviceCollection.AddMateralLogger(_configuration);
            _serviceProvider = _serviceCollection.BuildServiceProvider();
            _logger = _serviceProvider.GetRequiredService<ILogger<Program>>();
        }
        public static void Main()
        {
            const string message = "Hello World!";
            Debug.WriteLine($"[Debug]{message}");
            Trace.WriteLine($"[Trace]{message}");
            _logger.LogTrace(message);
            _logger.LogDebug(message);
            _logger.LogInformation(message);
            _logger.LogWarning(message);
            _logger.LogError(message);
            _logger.LogCritical(message);
        }
    }
}