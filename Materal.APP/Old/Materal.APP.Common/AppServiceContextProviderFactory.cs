using System;
using AspectCore.DependencyInjection;
using AspectCore.DynamicProxy;
using AspectCore.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.APP.Common
{
    [NonAspect]
    public class AppServiceContextProviderFactory : IServiceProviderFactory<IServiceContext>
    {
        public IServiceContext CreateBuilder(IServiceCollection services)
        {
            return services.ToServiceContext();
        }

        public IServiceProvider CreateServiceProvider(IServiceContext contextBuilder)
        {
            ApplicationService.ServiceProvider = contextBuilder.Build();
            return ApplicationService.ServiceProvider;
        }
    }
}
