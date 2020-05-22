using AspectCore.DependencyInjection;
using AspectCore.DynamicProxy;
using AspectCore.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Materal.Gateway
{
    [NonAspect]
    public class ServiceContextProviderFactory : IServiceProviderFactory<IServiceContext>
    {
        public IServiceContext CreateBuilder(IServiceCollection services)
        {
            return services.ToServiceContext();
        }

        public IServiceProvider CreateServiceProvider(IServiceContext contextBuilder)
        {
            ApplicationData.ServiceProvider = contextBuilder.Build();
            return ApplicationData.ServiceProvider;
        }
    }
}
