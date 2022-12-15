using Materal.ConvertHelper;
using Materal.Logger;
using Materal.Logger.CommandHandlers;
using Materal.Logger.Message;
using Materal.Logger.Models;

namespace Materal.LoggerClientHandler
{
    public class CommandBus
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
        public void Handler(MateralLoggerLocalServer server, WebSocketConnectionModel websocket, string message)
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
        /// <param name="event"></param>
        public void SendEvent(WebSocketConnectionModel websocket, IEvent @event)
        {
            websocket.SendMessage(@event.ToJson());
        }
    }
}
