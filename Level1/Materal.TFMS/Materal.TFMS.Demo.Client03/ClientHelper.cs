using Materal.Logger;
using Materal.TFMS.Demo.Client03.EventHandlers;
using Materal.TFMS.Demo.Core;
using Materal.TFMS.Demo.Core.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.TFMS.Demo.Client03
{
    public static class ClientHelper
    {
        private static readonly IServiceCollection _serviceCollection = new ServiceCollection();
        private static IServiceProvider _serviceProvider;
        public static string AppName { get; private set; } = "Client03";
        static ClientHelper()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .Build();
            _serviceCollection.AddMateralLogger();
            MateralLoggerManager.Init(null, configuration);
            MateralLoggerManager.CustomConfig.Add("ApplicationName", AppName);
            RegisterServices();
            _serviceProvider = _serviceCollection.BuildServiceProvider();
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
