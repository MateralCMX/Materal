using Materal.Gateway.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.Gateway.OcelotExtension
{
    public static class OcelotService
    {
        public static IServiceProvider? Service { get; set; }
        /// <summary>
        /// 获得服务或默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="GatewayException"></exception>
        public static IEnumerable<T> GetServices<T>()
        {
            if (Service == null) throw new GatewayException("服务未实例化");
            IEnumerable<T> result = Service.GetServices<T>();
            return result;
        }
        /// <summary>
        /// 获得服务或默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="GatewayException"></exception>
        public static T GetService<T>()
        {
            T? result = GetServiceOrDefault<T>();
            if (result == null) throw new GatewayException("找不到对象");
            return result;
        }
        /// <summary>
        /// 获得服务或默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="GatewayException"></exception>
        public static T? GetServiceOrDefault<T>()
        {
            if (Service == null) throw new GatewayException("服务未实例化");
            T? result = Service.GetService<T>();
            return result;
        }
        /// <summary>
        /// 获得服务或默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="GatewayException"></exception>
        public static T GetService<T>(Type type)
        {
            object? result = GetServiceOrDefault(type);
            if (result == null) throw new GatewayException("找不到对象");
            if(result is not T tResult) throw new GatewayException("对象类型错误");
            return tResult;
        }
        /// <summary>
        /// 获得服务或默认值
        /// </summary>
        /// <returns></returns>
        /// <exception cref="GatewayException"></exception>
        public static object GetService(Type type)
        {
            object? result = GetServiceOrDefault(type);
            if (result == null) throw new GatewayException("找不到对象");
            return result;
        }
        /// <summary>
        /// 获得服务或默认值
        /// </summary>
        /// <returns></returns>
        /// <exception cref="GatewayException"></exception>
        public static object? GetServiceOrDefault(Type type)
        {
            if (Service == null) throw new GatewayException("服务未实例化");
            object? result = Service.GetService(type);
            return result;
        }
    }
}
