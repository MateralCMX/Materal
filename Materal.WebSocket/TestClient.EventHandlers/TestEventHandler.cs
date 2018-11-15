using System;
using System.Threading;
using Materal.WebSocket.EventHandlers;
using Materal.WebSocket.Events;
using System.Threading.Tasks;
using TestClient.Common;

namespace TestClient.EventHandlers
{
    public class TestEventHandler : IEventHandler
    {
        public async Task ExcuteAsync(IEvent @event)
        {
            Thread.Sleep(500);
            ConsoleHelper.TestClientWriteLine(@event.StringData + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffff"), "Handler");
        }

        public void Excute(IEvent @event)
        {
            ExcuteAsync(@event).Wait();
        }
    }
}
