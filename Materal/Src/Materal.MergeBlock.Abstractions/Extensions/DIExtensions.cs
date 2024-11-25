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
        public static IServiceCollection AddMergeBlockHostedService<TBackgroundService>(this IServiceCollection services)
            where TBackgroundService : class, IHostedService
        {
            services.AddSingleton<IHostedService>(serviceProvider =>
            {
                IServiceProvider services = serviceProvider;
                Type type = typeof(TBackgroundService);
                ConstructorInfo[] constructorInfos = [.. type.GetConstructors().OrderByDescending(m => m.GetParameters().Length)];
                foreach (ConstructorInfo constructorInfo in constructorInfos)
                {
                    ParameterInfo[] parameterInfos = constructorInfo.GetParameters();
                    List<object?> objects = [];
                    for (int i = 0; i < parameterInfos.Length; i++)
                    {
                        object? temp = services.GetService(parameterInfos[i].ParameterType);
                        if (temp == null && !parameterInfos[i].HasDefaultValue) break;
                        objects.Add(temp);
                    }
                    if (parameterInfos.Length != objects.Count) continue;
                    object hostedServiceObj = constructorInfo.Invoke([.. objects]);
                    if (hostedServiceObj is not TBackgroundService hostedService) continue;
                    IEnumerable<IHostedServiceDecorator> backgroundServiceDecorators = services.GetServices<IHostedServiceDecorator>();
                    return new HostedServiceDecoratorHandlerService(hostedService, backgroundServiceDecorators);
                }
                throw new MergeBlockException($"实例化MergeBlockHostedService->{type.FullName}失败");
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
