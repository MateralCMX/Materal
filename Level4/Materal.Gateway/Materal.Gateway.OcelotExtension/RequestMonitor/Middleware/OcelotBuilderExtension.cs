using Materal.Gateway.OcelotExtension.RequestMonitor;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;

namespace Materal.Gateway.OcelotExtension
{
    public static partial class OcelotBuilderExtension
    {
        public static IOcelotBuilder AddRequestMonitor<T>(this IOcelotBuilder builder)
            where T : class, IRequestMonitorHandler
        {
            DefatultRequestMonitorHandlers.AddHandler<T>();
            builder.Services.AddSingleton<T>();
            return builder;
        }
        public static IOcelotBuilder AddRequestMonitor(this IOcelotBuilder builder, IRequestMonitorHandler customHandler)
        {
            DefatultRequestMonitorHandlers.AddHandler(customHandler.GetType());
            builder.Services.AddSingleton(customHandler);
            return builder;
        }
    }
}
