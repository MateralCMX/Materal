using Microsoft.Extensions.DependencyInjection;
using System;

namespace Materal.APP.Common
{
    public static class ApplicationService
    {
        public static IServiceProvider ServiceProvider { get; set; }
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
            return ServiceProvider != null ? ServiceProvider.GetService<T>() : default;
        }
    }
}
