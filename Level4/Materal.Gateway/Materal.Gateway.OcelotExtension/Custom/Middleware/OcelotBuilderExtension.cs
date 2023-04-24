using Materal.Gateway.OcelotExtension.Custom;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;

namespace Materal.Gateway.OcelotExtension
{
    public static partial class OcelotBuilderExtension
    {
        public static IOcelotBuilder AddCustomHandler<T>(this IOcelotBuilder builder)
            where T : class, ICustomHandler
        {
            DefaultCustomHandlers.AddHandler<T>();
            builder.Services.AddSingleton<T>();
            return builder;
        }
        public static IOcelotBuilder AddCustomHandler(this IOcelotBuilder builder, ICustomHandler customHandler)
        {
            DefaultCustomHandlers.AddHandler(customHandler.GetType());
            builder.Services.AddSingleton(customHandler);
            return builder;
        }
    }
}
