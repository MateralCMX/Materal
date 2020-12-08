using Demo.Events;
using DotNetty.Transport.Channels;
using Materal.DotNetty.EventBus;
using System;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Demo.EventHandlers
{
    public class ReplyMessageEventHandler : BaseEventHandler<ReplyMessageEvent>
    {
        public override Task HandlerAsync(ReplyMessageEvent @event, ClientWebSocket channel)
        {
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("从服务器收到消息:");
            Console.WriteLine(@event.Message);
            Console.WriteLine("-----------------------------------------------");
            return Task.CompletedTask;
        }
    }
}
