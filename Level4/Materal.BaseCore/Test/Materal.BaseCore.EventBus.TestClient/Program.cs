using Materal.BaseCore.Common;
using Materal.Logger.ConfigModels;
using Materal.Logger.ConsoleLogger;
using Materal.Logger.Extensions;
using Materal.TFMS.EventBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.BaseCore.EventBus.TestClient
{
    public class Program
    {
        public static void Main()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddMateralLogger(config =>
            {
                config.AddCustomConfig("ApplicationName", "TestClient");
                config.AddConsoleTarget("LifeConsole");
                config.AddAllTargetsRule();
            });
            const string queueName = "Educational_FatQueue";
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json"), optional: false, reloadOnChange: true);
            MateralCoreConfig.Configuration = builder.Build();
            services.AddEventBus(queueName, typeof(NewRegistrationFormEventHandler).Assembly);
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            IEventBus eventBus = serviceProvider.GetRequiredService<IEventBus>();
            eventBus.StartListening();
            Console.ReadKey();
        }
    }
}