using System;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Materal.WebSocket.Http
{
    public static class HttpDIExtension
    {
        public static IServiceCollection AddControllers(this IServiceCollection services, Func<Type,object> getParams, params Assembly[] assemblies)
        {
            IControllerBus controllerBus = new ControllerBusImpl();
            controllerBus.GetParams += getParams;
            foreach (Assembly assembly in assemblies)
            {
                foreach (Type type in assembly.ExportedTypes)
                {
                    if (type.Name.EndsWith("Controller"))
                    {
                        controllerBus.AddController(type);
                    }
                }
            }
            services.AddSingleton(controllerBus);
            return services;
        }
    }
}
