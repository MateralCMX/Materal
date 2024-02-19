using Materal.Oscillator.Abstractions.Works;
using Materal.Oscillator.QuartZExtend;
using Materal.Oscillator.Works;
using Microsoft.Extensions.Configuration;
using Quartz;

namespace Materal.Oscillator.Extensions
{
    /// <summary>
    /// DI扩展
    /// </summary>
    public static class OscillatorDIExtension
    {
        /// <summary>
        /// 添加Oscillator服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddOscillator(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddOscillator(null, assemblies);
            return services;
        }
        /// <summary>
        /// 添加Oscillator服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddOscillator(this IServiceCollection services, IConfiguration? configuration, params Assembly[] assemblies)
        {
            if (configuration != null)
            {
                OscillatorConfig.Init(configuration);
            }
            services.AddWorks(typeof(ConsoleWork).Assembly);
            foreach (Assembly assembly in assemblies)
            {
                services.AddWorks(assembly);
            }
            services.TryAddSingleton<IOscillatorHost, OscillatorHostImpl>();
            services.TryAddTransient<IJobListener, JobListener>();
            return services;
        }
        /// <summary>
        /// 添加作业
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IServiceCollection AddWorks(this IServiceCollection services, Assembly assembly)
        {
            services.AddWorks(assembly.GetTypes<IWork>().ToArray());
            return services;
        }
        /// <summary>
        /// 添加作业
        /// </summary>
        /// <param name="services"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public static IServiceCollection AddWorks(this IServiceCollection services, params Type[] types)
        {
            foreach (Type type in types)
            {
                services.AddWork(type);
            }
            return services;
        }
        /// <summary>
        /// 添加作业
        /// </summary>
        /// <param name="services"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IServiceCollection AddWork(this IServiceCollection services, Type type)
        {
            if (!type.IsClass || type.IsAbstract || !type.IsAssignableTo<IWork>()) return services;
            List<Type> allInterfaces = type.GetAllInterfaces();
            foreach (Type interfaces in allInterfaces)
            {
                if (!interfaces.IsGenericType || !interfaces.IsAssignableTo<IWork>()) continue;
                services.TryAddTransient(interfaces, type);
                break;
            }
            return services;
        }
    }
}
