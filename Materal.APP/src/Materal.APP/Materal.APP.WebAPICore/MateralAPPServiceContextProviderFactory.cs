using System;
using AspectCore.DependencyInjection;
using AspectCore.DynamicProxy;
using AspectCore.Extensions.DependencyInjection;
using Materal.APP.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.APP.WebAPICore
{
    [NonAspect]
    public class MateralAPPServiceContextProviderFactory : IServiceProviderFactory<IServiceContext>
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
