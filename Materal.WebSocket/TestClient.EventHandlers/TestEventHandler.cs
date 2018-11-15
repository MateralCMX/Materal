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
            ConsoleHelper.TestClientWriteLine(@event.StringData, "Handler");
        }

        public void Excute(IEvent @event)
        {
            ExcuteAsync(@event).Wait();
        }
    }
}
