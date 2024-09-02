using Microsoft.Extensions.DependencyInjection;

namespace Materal.Extensions
{
    /// <summary>
    /// 服务集合扩展
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// 获取单例实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static T? GetSingletonInstance<T>(this IServiceCollection services)
        {
            ServiceDescriptor? serviceDescriptor = services.FirstOrDefault(m => m.ServiceType == typeof(T));
            if (serviceDescriptor is null || serviceDescriptor.ImplementationInstance is not T result) return default;
            return result;
        }
    }
}