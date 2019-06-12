using Materal.Dispatcher.QuartzNet;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Logging;
using Quartz.Spi;
using System;
using System.Reflection;

namespace Materal.Dispatcher.Server
{
    public class ServerHelper
    {
        public static IServiceCollection Services;
        public static IServiceProvider ServiceProvider;
        static ServerHelper()
        {
            RegisterServices();
            BuildServices();
        }
        /// <summary>
        /// 注册依赖注入
        /// </summary>
        public static void RegisterServices()
        {
            Services = new ServiceCollection();
            Services.AddTransient<IDispatcherServer, DispatcherServerImpl>();
            Services.AddTransient<IDispatcherManager, DispatcherManager>();
            Services.AddTransient<IScheduleManager, ScheduleManager>();
            Services.AddTransient<ILogProvider, MyLogProvider>();
            Services.AddTransient<ITriggerListener, MyTriggerListener>();
            Services.AddTransient<ISchedulerListener, MySchedulerListener>();
            Services.AddTransient<IJobListener, MyJobListener>();
            Services.AddTransient<IJobFactory, MyJobFactory>();
            Assembly assembly = Assembly.Load("Materal.Dispatcher.Jobs");
            foreach (Type assemblyExportedType in assembly.ExportedTypes)
            {
                Services.AddTransient(assemblyExportedType);
            }
        }

        /// <summary>
        /// Build服务
        /// </summary>
        public static void BuildServices()
        {
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
