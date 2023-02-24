using Materal.Logger.Message;
using Materal.Logger.Models;

namespace Materal.Logger.CommandHandlers
{
    /// <summary>
    /// 命令总线
    /// </summary>
    public class CommandBus : ICommandBus
    {
        private readonly Dictionary<string, List<ICommandHander>> handers = new();
        /// <summary>
        /// 订阅
        /// </summary>
        public void Subscribe<TCommand, TCommandHander>()
            where TCommand : ICommand
            where TCommandHander : ICommandHander<TCommand>, new()
        {
            string key = typeof(TCommand).Name;
            if (!handers.ContainsKey(key))
            {
                handers.Add(key, new List<ICommandHander>());
            }
            handers[key].Add(new TCommandHander());
        }
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="server"></param>
        /// <param name="websocket"></param>
        /// <param name="message"></param>
        public void Handler(LoggerLocalServer server, WebSocketConnectionModel websocket, string message)
        {
            try
            {
                BaseCommand baseCommand = message.JsonToObject<BaseCommand>();
                if (!handers.ContainsKey(baseCommand.CommandName)) return;
                ICommand command = (ICommand)message.JsonToObject(baseCommand.CommandName);
                foreach (ICommandHander item in handers[baseCommand.CommandName])
                {
                    item.Handler(server, websocket, command);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("处理Command失败");
            }
        }
        /// <summary>
        /// 发送事件
        /// </summary>
        /// <param name="websocket"></param>
        /// <param name="event"></param>
        public void SendEvent(WebSocketConnectionModel websocket, IEvent @event)
        {
            websocket.SendMessage(@event.ToJson());
        }
    }
}
