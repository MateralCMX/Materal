using Materal.TFMS.Demo.Client02.EventHandlers;
using Materal.TFMS.Demo.Core;
using Materal.TFMS.Demo.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Extensions.Logging;
using RabbitMQ.Client;
using System;
using Materal.TFMS.EventBus;
using Materal.TFMS.EventBus.RabbitMQ;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace Materal.TFMS.Demo.Client02
{
    public static class ClientHelper
    {
        public static IServiceCollection Services { get; set; }
        public static IServiceProvider ServiceProvider { get; private set; }
        public static string AppName { get; set; }
        static ClientHelper()
        {
            RegisterServices();
            BuildServices();
            Configure();
        }
        /// <summary>
        /// 注册依赖注入
        /// </summary>
        public static void RegisterServices()
        {
            AppName = "Client02";
            Services = new ServiceCollection();
            Services.AddTransient<IConnectionFactory, ConnectionFactory>(serviceProvider => new ConnectionFactory
            {
                HostName = "127.0.0.1",
                DispatchConsumersAsync = true,
                UserName = "admin",
                Password = "admin"
            });
            Services.AddTransient<ILoggerFactory, LoggerFactory>();
            const string queueName = "MateralTFMSDemoQueueName2";
            const string exchangeName = "MateralTFMSDemoExchangeName";
            Services.AddEventBus(queueName, exchangeName);
            Services.AddSingleton<IClient, ClientImpl>();
            Services.AddTransient<Client02Event01Handler>();
            Services.AddTransient<Client02Event01Handler2>();
            Services.AddTransient<Client02Event03Handler>();
            Services.AddTransient<Client02Event03Handler2>();
            Services.AddLogging(builder =>
            {
                builder.SetMinimumLevel(LogLevel.Trace);
                builder.AddNLog(new NLogProviderOptions
                {
                    CaptureMessageTemplates = true,
                    CaptureMessageProperties = true
                });
            });
        }
        /// <summary>
        /// Build服务
        /// </summary>
        public static void BuildServices()
        {
            Build();
        }
        /// <summary>
        /// 配置
        /// </summary>
        public static void Configure()
        {
            LogManager.LoadConfiguration("NLog.config");
            LogManager.Configuration.Install(new InstallationContext());
            LogManager.Configuration.Variables["AppName"] = AppName;
            var factory = ServiceProvider.GetService<ILoggerFactory>();
            factory.AddNLog();
        }
        /// <summary>
        /// 生成
        /// </summary>
        public static void Build()
        {
            if (Services == null) throw new InvalidOperationException("依赖注入服务未找到");
            ServiceProvider = Services.BuildServiceProvider();
        }
        /// <summary>
        /// 获得服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetService<T>()
        {
            return ServiceProvider.GetService<T>();
        }
    }
}
