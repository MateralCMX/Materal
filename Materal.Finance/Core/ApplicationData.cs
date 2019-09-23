using Microsoft.Extensions.DependencyInjection;
using System;
using AspectCore.Extensions.DependencyInjection;

namespace Core
{
    public class ApplicationData
    {
        /// <summary>
        /// 依赖注入服务
        /// </summary>
        public static IServiceCollection Services { get; set; }
        /// <summary>
        /// 依赖注入服务
        /// </summary>
        public static IServiceProvider ServiceProvider { get; private set; }
        /// <summary>
        /// APP名称
        /// </summary>
        public static string AppName { get; set; }
        /// <summary>
        /// 打包
        /// </summary>
        public static void Build()
        {
            if (Services == null)
            {
                throw new InvalidOperationException("依赖注入服务未找到");
            }
            ServiceProvider = Services.BuildDynamicProxyServiceProvider();
        }
    }
}
