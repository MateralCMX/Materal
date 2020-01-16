using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Materal.DotNetty.CommandBus
{
    public static class CommandDIExtension
    {

        /// <summary>
        /// 添加命令总线服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        public static void AddCommandBus(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddSingleton<ICommandBus, CommandBusImpl>();
            services.AddCommand(assemblies);
        }

        /// <summary>
        /// 添加命令服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        private static void AddCommand(this IServiceCollection services, params Assembly[] assemblies)
        {
            var commandHandlerHelper = new CommandHandlerHelper();
            Type commandHandlerType = typeof(ICommandHandler);
            foreach (Assembly assembly in assemblies)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (!commandHandlerType.IsAssignableFrom(type)) continue;
                    if (commandHandlerHelper.TryAddCommandHandler(type))
                    {
                        services.AddTransient(type);
                    }
                }
            }
            services.AddSingleton(commandHandlerHelper);
        }
    }
}
