using Materal.TFMS.Demo.Client01.EventHandlers;
using Materal.TFMS.Demo.Core;
using Materal.TFMS.Demo.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Extensions.Logging;
using RabbitMQ.Client;
using System;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace Materal.TFMS.Demo.Client01
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
            AppName = "Client01";
            Services = new ServiceCollection();
            Services.AddTransient<IConnectionFactory, ConnectionFactory>(serviceProvider => new ConnectionFactory
            {
                HostName = "127.0.0.1",
                DispatchConsumersAsync = true,
                UserName = "admin",
                Password = "admin"
            });
            Services.AddTransient<ILoggerFactory, LoggerFactory>();
            const string queueName = "MateralTFMSDemoQueueName1";
            const string exchangeName = "MateralTFMSDemoExchangeName";
            Services.AddEventBus(queueName, exchangeName);
            Services.AddSingleton<IClient, ClientImpl>();
            Services.AddTransient<Client01Event01Handler>();
            Services.AddTransient<Client01Event02Handler>();
            Services.AddTransient<Client01Event02Handler2>();
            Services.AddTransient<Client01Event03Handler>();
            Services.AddTransient<Client01Event03Handler2>();
            Services.AddTransient<Client01Event04Handler>();
            Services.AddTransient<Client01Event05Handler>();
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
