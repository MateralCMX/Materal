using Materal.WebSocket.EventHandlers;
using Materal.WebSocket.Events;
using System.Threading.Tasks;
using TestWebSocket.Common;

namespace TestWebSocket.EventHandlers
{
    public class TestEventHandler : IEventHandler
    {
        public async Task ExcuteAsync(IEvent @event)
        {
            await Task.Delay(1000);
            ConsoleHelper.TestWriteLine(@event.HandlerName, "接受到事件");
        }

        public void Excute(IEvent @event)
        {
            ExcuteAsync(@event).Wait();
        }
    }
}
