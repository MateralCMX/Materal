using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Materal.Utils.CloudStorage.Tencent
{
    /// <summary>
    /// 服务扩展
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// 添加腾讯云存储
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddTencentCloudStorage(this IServiceCollection services)
        {
            services.TryAddSingleton<TencentCloudStorageService>();
            return services;
        }
    }
}
