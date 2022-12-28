using Materal.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace Materal.Logger
{
    public static class MateralLoggerDIExtensions
    {
        public static IServiceCollection AddMateralLogger(this IServiceCollection services)
        {
            services.AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.AddFilter<MateralLoggerProvider>(null, LogLevel.Trace);
            });
            services.Replace(ServiceDescriptor.Singleton<ILoggerFactory, MateralLoggerFactory>());
            services.Replace(ServiceDescriptor.Singleton<ILoggerProvider, MateralLoggerProvider>());
            return services;
        }
        public static IApplicationBuilder UseMateralLogger(this IApplicationBuilder app, Action<MateralLoggerConfigOptions>? option = null, IConfiguration? configuration = null)
        {
            MateralLoggerManager.Init(option, configuration);
            return app;
        }
    }
}
