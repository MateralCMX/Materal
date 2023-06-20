using Materal.BaseCore.Common;
using Materal.Logger;
using Materal.TFMS.EventBus;
using Materal.TFMS.EventBus.Extensions;
using Materal.TFMS.EventBus.RabbitMQ;
using Materal.TFMS.EventBus.RabbitMQ.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Reflection;

namespace Materal.BaseCore.EventBus.TestClient
{
    public class Program
    {
        public static async Task Main()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddMateralLogger();
            LoggerManager.Init(option =>
            {
                option.AddConsoleTarget("LifeConsole");
                option.AddAllTargetRule();
            });
            LoggerManager.CustomConfig.Add("ApplicationName", "TestClient");
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