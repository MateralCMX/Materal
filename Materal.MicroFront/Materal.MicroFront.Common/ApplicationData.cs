using System;
using AspectCore.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.MicroFront.Common
{
    public static class ApplicationData
    {
        private static IServiceCollection Services;
        private static IServiceProvider ServiceProvider;
        /// <summary>
        /// 注册依赖注入
        /// </summary>
        public static void RegisterServices(Action<IServiceCollection> addService)
        {
            Services = new ServiceCollection();
            addService?.Invoke(Services);
        }
        /// <summary>
        /// Build服务
        /// </summary>
        public static void BuildServices()
        {
            if (Services == null)
            {
                throw new InvalidOperationException("依赖注入服务未找到");
            }
            ServiceProvider = Services.BuildDynamicProxyServiceProvider();
        }
        /// <summary>
        /// 获得服务
        /// </summary>
        /// <returns></returns>
        public static object GetService(Type type)
        {
            return ServiceProvider.GetService(type);
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
