using Materal.DotNetty.Server.Core;
using System;

namespace Materal.DotNetty.CommandBus
{
    public class CommandBusImpl : ICommandBus
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly CommandHandlerHelper _commandHandlerHelper;

        public CommandBusImpl(CommandHandlerHelper commandHandlerHelper, IServiceProvider serviceProvider)
        {
            _commandHandlerHelper = commandHandlerHelper;
            _serviceProvider = serviceProvider;
        }

        public ICommandHandler GetCommandHandler(string commandHandlerName)
        {
            Type type = _commandHandlerHelper.GetCommandHandler(commandHandlerName);
            if (type == null) throw new DotNettyServerException("未找到命令处理器");
            object service = _serviceProvider.GetService(type);
            if (!(service is ICommandHandler commandHandler)) throw new DotNettyServerException($"命令处理器必须实现接口{nameof(ICommandHandler)}");
            return commandHandler;
        }
    }
}
