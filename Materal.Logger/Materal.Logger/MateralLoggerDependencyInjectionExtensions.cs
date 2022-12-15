using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace Materal.Logger
{
    public static class MateralLoggerDependencyInjectionExtensions
    {
        public static IServiceCollection AddMateralLogger(this IServiceCollection services, IConfiguration? configuration = null)
        {
            MateralLoggerConfig.Init(configuration);
            services.AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.AddFilter<MateralLoggerProvider>(null, LogLevel.Trace);
            });
            services.Replace(ServiceDescriptor.Singleton<ILoggerFactory, MateralLoggerFactory>());
            services.Replace(ServiceDescriptor.Singleton<ILoggerProvider, MateralLoggerProvider>());
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            MateralLogger.InitServer();
            return services;
        }

        private static void CurrentDomain_ProcessExit(object? sender, EventArgs e)
        {
            MateralLoggerManager.Shutdown();
        }
    }
}
