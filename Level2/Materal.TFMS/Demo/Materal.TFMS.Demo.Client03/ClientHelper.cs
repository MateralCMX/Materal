using Materal.Logger.ConfigModels;
using Materal.Logger.ConsoleLogger;
using Materal.Logger.Extensions;
using Materal.TFMS.Demo.Client03.EventHandlers;
using Materal.TFMS.Demo.Core;
using Materal.TFMS.Demo.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.TFMS.Demo.Client03
{
    public static class ClientHelper
    {
        private static readonly IServiceCollection _serviceCollection = new ServiceCollection();
        private static readonly IServiceProvider _serviceProvider;
        public static string AppName { get; private set; } = "Client03";
        static ClientHelper()
        {
            _serviceCollection.AddMateralLogger(config =>
            {
                config.AddCustomConfig("ApplicationName", AppName);
                config.AddConsoleTarget("LifeConsole");
                config.AddAllTargetsRule();
            });
            RegisterServices();
            _serviceProvider = _serviceCollection.BuildServiceProvider();
            _serviceProvider.UseMateralLoggerAsync().Wait();
        }
        /// <summary>
        /// 注册依赖注入
        /// </summary>
        public static void RegisterServices()
        {
            const string queueName = "MateralTFMSDemoQueueName3";
            _serviceCollection.AddEventBus(queueName, ConnectionHelper.ExchangeName);
            _serviceCollection.AddSingleton<IClient, ClientImpl>();
            _serviceCollection.AddTransient<Client03Event01Handler>();
            _serviceCollection.AddTransient<Client03Event01Handler2>();
            _serviceCollection.AddTransient<Client03Event02Handler>();
            _serviceCollection.AddTransient<Client03Event02Handler2>();
        }
        /// <summary>
        /// 获得服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetService<T>()
        {
            return _serviceProvider.GetService<T>() ?? throw new ApplicationException("服务未找到");
        }
    }
}
