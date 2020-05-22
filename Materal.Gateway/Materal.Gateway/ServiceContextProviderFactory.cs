using AspectCore.DependencyInjection;
using AspectCore.DynamicProxy;
using AspectCore.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Materal.Gateway
{
    /// <summary>
    /// 服务工厂
    /// </summary>
    [NonAspect]
    public class ServiceContextProviderFactory : IServiceProviderFactory<IServiceContext>
    {
        /// <summary>
        /// 创建构建器
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceContext CreateBuilder(IServiceCollection services)
        {
            return services.ToServiceContext();
        }
        /// <summary>
        /// 创建容器
        /// </summary>
        /// <param name="contextBuilder"></param>
        /// <returns></returns>
        public IServiceProvider CreateServiceProvider(IServiceContext contextBuilder)
        {
            ApplicationData.ServiceProvider = contextBuilder.Build();
            return ApplicationData.ServiceProvider;
        }
    }
}
