using Materal.WebSocket.Client;
using Materal.WebSocket.Client.Model;
using Materal.WebSocket.Events;
using System;
using System.Text;
using System.Threading.Tasks;
using Materal.ConvertHelper;
using TestWebSocket.Events;

namespace TestClient.WebSocketClient.WebStock
{
    public class TestWebStockHandler : WebSocketClientHandler
    {
        public override void ChannelActive(WebSocketClientImpl channel)
        {
            Console.WriteLine("客户端已连接");
        }

        public override void ChannelInactive(WebSocketClientImpl channel)
        {
            Console.WriteLine("客户端已断开连接");
            Task.Factory.StartNew(channel.StartAsync);
        }

        public override void ChannelRead0(WebSocketClientImpl channel, byte[] msg)
        {
            string message = Encoding.UTF8.GetString(msg);
            IEvent @event = message.JsonToObject<Event>();
            channel.HandleEvent(@event);
            Console.WriteLine(message);
        }

        public override void ExceptionCaught(WebSocketClientImpl channel, Exception ex)
        {
            Console.WriteLine("Exception: " + ex.Message);
        }

        public override void ChannelStart(WebSocketClientImpl channel)
        {
            Console.WriteLine("正在连接服务器");
        }

        public override void OnSendMessage(object message)
        {
            Console.WriteLine(message);
        }
    }
}
