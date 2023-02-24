using Materal.Core.Logger.CommandHandlers;
using Materal.Logger.Message;
using System.Net.WebSockets;
using System.Text;

namespace Materal.LoggerClient.EventHandlers
{
    public class EventBus
    {
        private readonly Dictionary<string, List<IEventHandler>> handers = new();
        /// <summary>
        /// 订阅
        /// </summary>
        public void Subscribe<TEvent, TEventHandler>()
            where TEvent : IEvent
            where TEventHandler : IEventHandler<TEvent>, new()
        {
            string key = typeof(TEvent).Name;
            if (!handers.ContainsKey(key))
            {
                handers.Add(key, new List<IEventHandler>());
            }
            handers[key].Add(new TEventHandler());
        }
        public void Handler(string message)
        {
            try
            {
                BaseEvent baseEvent = message.JsonToObject<BaseEvent>();
                if (!handers.ContainsKey(baseEvent.EventName)) return;
                IEvent command = (IEvent)message.JsonToObject(baseEvent.EventName);
                foreach (IEventHandler item in handers[baseEvent.EventName])
                {
                    item.Handler(command);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("处理Event失败");
            }
        }
        /// <summary>
        /// 发送事件
        /// </summary>
        /// <param name="event"></param>
        public async Task SendCommandAsync(ClientWebSocket webSocket ,ICommand command)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(command.ToJson());
            await webSocket.SendAsync(messageBytes, WebSocketMessageType.Text, true, CancellationToken.None);
        }
    }
}
