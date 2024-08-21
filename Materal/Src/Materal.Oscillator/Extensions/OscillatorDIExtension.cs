using Materal.Oscillator.Abstractions.Oscillators;
using Materal.Oscillator.Abstractions.PlanTriggers;
using Materal.Oscillator.Abstractions.PlanTriggers.DateTriggers;
using Materal.Oscillator.Abstractions.PlanTriggers.TimeTriggers;
using Materal.Oscillator.Abstractions.Works;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

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
            services.TryAddSingleton<IOscillatorHost, OscillatorHost>();
            Assembly[] trueAssemblies = assemblies.Union([typeof(OscillatorDIExtension).Assembly]).ToArray();
            foreach (Assembly assembly in trueAssemblies)
            {
                try
                {
                    foreach (Type type in assembly.GetTypesByFilter(m => m.IsClass && !m.IsAbstract))
                    {
                        if (type.IsAssignableTo<IOscillator>())
                        {
                            services.AddSingleton(typeof(IOscillator), type);
                            services.AddTransient(type);
                        }
                        else if (type.IsAssignableTo<IWork>())
                        {
                            services.AddTransient(typeof(IWork), type);
                            services.AddTransient(type);
                        }
                        else if (type.IsAssignableTo<IPlanTrigger>())
                        {
                            services.AddTransient(typeof(IPlanTrigger), type);
                            services.AddTransient(type);
                        }
                        else if (type.IsAssignableTo<IDateTrigger>())
                        {
                            services.AddTransient(typeof(IDateTrigger), type);
                            services.AddTransient(type);
                        }
                        else if (type.IsAssignableTo<ITimeTrigger>())
                        {
                            services.AddTransient(typeof(ITimeTrigger), type);
                            services.AddTransient(type);
                        }
                    }
                }
                catch
                {
                }
            }
            return services;
        }
        /// <summary>
        /// 添加Oscillator服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddOscillator(this IServiceCollection services)
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            services.AddOscillator(assemblies);
            return services;
        }
        /// <summary>
        /// 使用Oscillator
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static async Task<IServiceProvider> UseOscillatorAsync(this IServiceProvider services)
        {
            OscillatorServices.ServiceProvider = services;
            IOscillatorHost oscillatorHost = services.GetRequiredService<IOscillatorHost>();
            await oscillatorHost.StartAsync();
            return services;
        }
    }
}
