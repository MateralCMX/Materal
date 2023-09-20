using Materal.Logger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MainDemo
{
    public partial class Program
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
            _serviceCollection.AddMateralLogger(options =>
            {
                options.AddCustomConfig("ApplicationName", "MainDemo");
            });
            _serviceProvider = _serviceCollection.BuildServiceProvider();
            _logger = _serviceProvider.GetRequiredService<ILogger<Program>>();
        }
    }
}
