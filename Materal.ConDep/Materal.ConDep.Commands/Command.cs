using Materal.WebSocket.Commands;
using System;

namespace Materal.ConDep.Commands
{
    [Serializable]
    public class Command : ICommand
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public Command()
        {
            HandlerName = GetType().Name + "Handler";
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="handlerName"></param>
        public Command(string handlerName)
        {
            HandlerName = handlerName;
        }
        /// <summary>
        /// 处理器名称
        /// </summary>
        public string HandlerName { get; set; }
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
    }
}
