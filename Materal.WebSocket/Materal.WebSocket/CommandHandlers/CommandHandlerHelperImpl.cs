using Materal.WebSocket.CommandHandlers.Model;
using System;
using System.Collections.Generic;

namespace Materal.WebSocket.CommandHandlers
{
    public sealed class CommandHandlerHelperImpl : ICommandHandlerHelper
    {
        /// <summary>
        /// 命令类型
        /// </summary>
        private readonly List<Type> _commandTypes;

        public CommandHandlerHelperImpl(List<Type> types) => _commandTypes = types;

        public IEnumerable<Type> GetAllHandlerTypes()
        {
            return _commandTypes;
        }
        public Type GetHandlerType(string handlerName)
        {
            IEnumerable<Type> allHandler = GetAllHandlerTypes();
            foreach (Type item in allHandler)
            {
                if (item.Name.Equals(handlerName, StringComparison.OrdinalIgnoreCase))
                {
                    return item;
                }
            }
            throw new CommandHandlerException($"未找到处理器{handlerName}");
        }
    }
}
