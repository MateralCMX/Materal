using Materal.WebSocket.CommandHandlers;
using Materal.WebSocket.Commands;
using Materal.WebSocket.EventHandlers;
using Materal.WebSocket.Events;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Materal.WebSocket
{
    public static class ServiceCollectionExtend
    {
        private static readonly object CommandLocker = new object();
        public static void AddCommandBus(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddSingleton<ICommandBus, CommandBusImpl>();
            services.AddCommandHandlersSingleton(assemblies);
        }
        public static void AddCommandHandlersSingleton(this IServiceCollection services, params Assembly[] assemblies)
        {
            var commandHandlerTypes = new List<Type>();
            foreach (Assembly item in assemblies)
            {
                lock (CommandLocker)
                {
                    commandHandlerTypes.AddRange(GetCommandHandlerTypes(item));
                }
            }
            ICommandHandlerHelper implementationInstance = new CommandHandlerHelperImpl(commandHandlerTypes);
            services.AddSingleton(implementationInstance);
        }
        private static IEnumerable<Type> GetCommandHandlerTypes(Assembly assembly)
        {
            var result = new List<Type>();
            Type ihandlerType = typeof(ICommandHandler);
            Type[] assemblyTypes = assembly.GetTypes();
            foreach (Type item in assemblyTypes)
            {
                Type[] interfaceTypes = item.GetInterfaces();
                result.AddRange(from interfaceType in interfaceTypes where interfaceType.GUID == ihandlerType.GUID select item);
            }
            return result;
        }
        private static readonly object EventLocker = new object();
        public static void AddEventBus(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddSingleton<IEventBus, EventBusImpl>();
            services.AddEventHandlersSingleton(assemblies);
        }
        public static void AddEventHandlersSingleton(this IServiceCollection services, params Assembly[] assemblies)
        {
            var commandHandlerTypes = new List<Type>();
            foreach (Assembly item in assemblies)
            {
                lock (EventLocker)
                {
                    commandHandlerTypes.AddRange(GetEventHandlerTypes(item));
                }
            }
            IEventHandlerHelper implementationInstance = new EventHandlerHelperImpl(commandHandlerTypes);
            services.AddSingleton(implementationInstance);
        }
        private static IEnumerable<Type> GetEventHandlerTypes(Assembly assembly)
        {
            var result = new List<Type>();
            Type ihandlerType = typeof(IEventHandler);
            Type[] assemblyTypes = assembly.GetTypes();
            foreach (Type item in assemblyTypes)
            {
                Type[] interfaceTypes = item.GetInterfaces();
                result.AddRange(from interfaceType in interfaceTypes where interfaceType.GUID == ihandlerType.GUID select item);
            }
            return result;
        }

    }
}
