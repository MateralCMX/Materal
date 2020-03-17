using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Materal.DotNetty.EventBus
{
    public static class EventDIExtension
    {

        /// <summary>
        /// 添加事件总线服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        public static void AddEventBus(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddSingleton<IEventBus, EventBusImpl>();
            services.AddEvent(assemblies);
        }

        /// <summary>
        /// 添加事件服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        private static void AddEvent(this IServiceCollection services, params Assembly[] assemblies)
        {
            var eventHandlerHelper = new EventHandlerHelper();
            Type eventHandlerType = typeof(IEventHandler);
            foreach (Assembly assembly in assemblies)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (!eventHandlerType.IsAssignableFrom(type)) continue;
                    if (eventHandlerHelper.TryAddEventHandler(type))
                    {
                        services.AddTransient(type);
                    }
                }
            }
            services.AddSingleton(eventHandlerHelper);
        }
    }
}
