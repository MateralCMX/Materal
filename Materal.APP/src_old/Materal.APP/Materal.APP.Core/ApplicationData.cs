using Microsoft.Extensions.DependencyInjection;
using System;

namespace Materal.APP.Core
{
    /// <summary>
    /// 应用程序数据
    /// </summary>
    public class ApplicationData
    {
        /// <summary>
        /// 依赖注入服务
        /// </summary>
        public static IServiceProvider ServiceProvider { get; set; }
        /// <summary>
        /// APP名称
        /// </summary>
        public static string AppName { get; set; }
        /// <summary>
        /// 获得服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetService<T>(Type type)
        {
            return (T)ServiceProvider.GetService(type);
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
