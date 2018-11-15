using Materal.WebSocket.Client.Model;
using System.Threading.Tasks;
using Materal.WebSocket.Commands;
using Materal.WebSocket.Events;

namespace Materal.WebSocket.Client
{
    /// <summary>
    /// WebSocket客户端
    /// </summary>
    public interface IWebSocketClient
    {
        IWebSocketClientConfig Config { get; }

        void SetConfig(IWebSocketClientConfig config);

        Task StartAsync<T>() where T : IWebSocketClientHandler;

        Task StartAsync();

        Task StopAsync();

        Task SendCommandAsync(ICommand command);

        Task SendCommandByStringAsync(ICommand command);

        Task SendCommandByBytesAsync(ICommand command);

        Task HandleEventAsync(IEvent eventM);

        void HandleEvent(IEvent eventM);
    }
}
