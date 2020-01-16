using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Materal.DotNetty.ControllerBus
{
    public static class ControllerDIExtension
    {
        /// <summary>
        /// 添加控制器总线服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        public static void AddControllerBus(this IServiceCollection services, params Assembly[] assemblies)
        {
            AddControllerBus(services, null, assemblies);
        }

        /// <summary>
        /// 添加控制器总线服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action"></param>
        /// <param name="assemblies"></param>
        public static void AddControllerBus(this IServiceCollection services, Action<ControllerHelper> action, params Assembly[] assemblies)
        {
            services.AddSingleton<IControllerBus, ControllerBusImpl>();
            services.AddController(action, assemblies);
        }

        /// <summary>
        /// 添加控制器服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action"></param>
        /// <param name="assemblies"></param>
        private static void AddController(this IServiceCollection services, Action<ControllerHelper> action, params Assembly[] assemblies)
        {
            var controllerHelper = new ControllerHelper();
            Type baseControllerType = typeof(BaseController);
            foreach (Assembly assembly in assemblies)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (!type.IsSubclassOf(baseControllerType)) continue;
                    if (controllerHelper.TryAddController(type))
                    {
                        services.AddTransient(type);
                    }
                }
            }
            action?.Invoke(controllerHelper);
            services.AddSingleton(controllerHelper);
        }
    }
}
