using Microsoft.Extensions.Hosting;

namespace Materal.MergeBlock.Abstractions.Extensions
{
    /// <summary>
    /// DI扩展
    /// </summary>
    public static class DIExtensions
    {
        /// <summary>
        /// 添加一个带有装饰器的托管服务
        /// </summary>
        /// <typeparam name="TBackgroundService"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddHostedServiceWithDecorators<TBackgroundService>(this IServiceCollection services)
            where TBackgroundService : class, IHostedService
        {
            services.AddSingleton<TBackgroundService>();
            services.AddSingleton<IHostedService>(provider =>
            {
                TBackgroundService hostedService = provider.GetRequiredService<TBackgroundService>();
                IEnumerable<IHostedServiceDecorator> backgroundServiceDecorators = provider.GetServices<IHostedServiceDecorator>();
                return new HostedServiceDecoratorHandlerService(hostedService, backgroundServiceDecorators);
            });
            return services;
        }
        /// <summary>
        /// 添加一个托管服务装饰器
        /// </summary>
        /// <typeparam name="TBackgroundServiceDecorator"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddHostedServiceDecorator<TBackgroundServiceDecorator>(this IServiceCollection services)
            where TBackgroundServiceDecorator : class, IHostedServiceDecorator
        {
            services.AddSingleton<IHostedServiceDecorator, TBackgroundServiceDecorator>();
            return services;
        }
    }
}
