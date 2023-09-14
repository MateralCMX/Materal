using Microsoft.Extensions.DependencyInjection;

namespace Materal.Extensions.DependencyInjection
{
    /// <summary>
    /// DI扩展
    /// </summary>
    public static class DIExtension
    {
        /// <summary>
        /// 构建Materal服务提供者
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceProvider BuildMateralServiceProvider(this IServiceCollection services)
        {
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            return new MateralServiceProvider(serviceProvider);
        }
    }
}
