using Materal.Logger;
using Materal.TFMS.Demo.Client01.EventHandlers;
using Materal.TFMS.Demo.Core;
using Materal.TFMS.Demo.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.TFMS.Demo.Client01
{
    public static class ClientHelper
    {
        private static readonly IServiceCollection _serviceCollection = new ServiceCollection();
        private static readonly IServiceProvider _serviceProvider;
        public static string AppName { get; private set; } = "Client01";
        static ClientHelper()
        {
            _serviceCollection.AddMateralLogger(option =>
            {
                option.AddCustomConfig("ApplicationName", AppName);
                option.AddConsoleTarget("LifeConsole");
                option.AddAllTargetRule();
            });
            RegisterServices();
            _serviceProvider = _serviceCollection.BuildServiceProvider();
        }
        /// <summary>
        /// 注册依赖注入
        /// </summary>
        public static void RegisterServices()
        {
            const string queueName = "MateralTFMSDemoQueueName1";
            _serviceCollection.AddEventBus(queueName, ConnectionHelper.ExchangeName);
            _serviceCollection.AddSingleton<IClient, ClientImpl>();
            _serviceCollection.AddTransient<Client01Event02Handler>();
            _serviceCollection.AddTransient<Client01Event02Handler2>();
            _serviceCollection.AddTransient<Client01Event03Handler>();
            _serviceCollection.AddTransient<Client01Event03Handler2>();
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
