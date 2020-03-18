using System;
using Materal.DotNetty.Server.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.ConfigCenter.ConfigServer.Common
{
    public static class ApplicationService
    {
        private static IServiceCollection Services;
        private static IServiceProvider ServiceProvider;
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="actions"></param>
        public static void RegisterServices(params Action<IServiceCollection>[] actions)
        {
            Services = new ServiceCollection();
            foreach (Action<IServiceCollection> action in actions)
            {
                action?.Invoke(Services);
            }
        }
        /// <summary>
        /// 构建服务
        /// </summary>
        public static void BuildServices()
        {
            if (Services == null)
            {
                throw new DotNettyServerException("服务未注册，请先调用RegisterServices方法");
            }
            ServiceProvider = Services.BuildServiceProvider();
        }
        /// <summary>
        /// 获得服务
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object GetService(Type type)
        {
            return ServiceProvider.GetService(type);
        }
        /// <summary>
        /// 获得服务
        /// </summary>
        /// <returns></returns>
        public static T GetService<T>()
        {
            return ServiceProvider.GetService<T>();
        }
    }
}
