using Materal.WebSocket.Commands;
using Materal.WebSocket.Events;
using System.Threading.Tasks;

namespace Materal.WebSocket.Server
{
    public interface IWebSocketServer
    {

        Task SendEventAsync(IEvent @event);

        Task SendEventByStringAsync(IEvent @event);

        Task SendEventByBytesAsync(IEvent @event);

        Task HandleCommandAsync(ICommand command);

        void HandleCommand(ICommand command);
    }
}
