using DotNetty.Transport.Channels;
using Materal.WebSocket.CommandHandlers;
using Materal.WebSocket.CommandHandlers.Model;
using Materal.WebSocket.Commands.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Materal.Common;

namespace Materal.WebSocket.Commands
{
    public class CommandBusImpl : ICommandBus
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ICommandHandlerHelper _handlerHelper;
        public CommandBusImpl(IServiceProvider serviceProvider, ICommandHandlerHelper handlerHelper)
        {
            _serviceProvider = serviceProvider;
            _handlerHelper = handlerHelper;
        }
        public void Send(IChannelHandlerContext ctx, object commandData, ICommand command)
        {
            GetHandler(command.HandlerName).Excute(ctx, commandData);
        }
        public async Task SendAsync(IChannelHandlerContext ctx, object commandData, ICommand command)
        {
            await GetHandler(command.HandlerName).ExcuteAsync(ctx, commandData);
        }

        public List<CommandHandlerModel> GetAllCommandHandler()
        {
            var result = new List<CommandHandlerModel>();
            IEnumerable<Type> handlerTypes = _handlerHelper.GetAllHandlerTypes();
            foreach (Type handlerType in handlerTypes)
            {
                object[] attributes = handlerType.GetCustomAttributes(typeof(DescriptionAttribute), false);
                CommandHandlerModel temp;
                if (attributes.Length > 0)
                {
                    var handler = (ICommandHandler) ActivatorUtilities.CreateInstance(_serviceProvider, handlerType);
                    temp = new CommandHandlerModel
                    {
                        Description = handler.GetDescription(),
                        HandlerName = handlerType.Name
                    };
                }
                else
                {
                    temp = new CommandHandlerModel
                    {
                        Description = "",
                        HandlerName = handlerType.Name
                    };
                }
                result.Add(temp);
            }
            return result;
        }

        /// <summary>
        /// 获得处理器
        /// </summary>
        /// <param name="handlerName">处理器名称</param>
        /// <returns>处理器对象</returns>
        public ICommandHandler GetHandler(string handlerName)
        {
            Type handlerType = _handlerHelper.GetHandlerType(handlerName);
            var handler = (ICommandHandler)ActivatorUtilities.CreateInstance(_serviceProvider, handlerType);
            if (handler == null) throw new CommandException("未找到对应处理器");
            return handler;
        }
    }
}
