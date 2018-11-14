using System.Threading.Tasks;
using Materal.WebSocket.Client.Model;
using Materal.WebSocket.Commands;

namespace Materal.WebSocket.Client
{
    /// <summary>
    /// WebSocket客户端
    /// </summary>
    public interface IWebSocketClient
    {
        //event ConfigEvent OnConfigChange;

        //event ConnectServerEvent OnStateChange;

        //event ReceiveEventEvent OnReceiveEvent;

        //event SendCommandEvent OnSendCommand;

        WebSocketClientConfigModel Config { get; }

        WebSocketClientStateEnum State { get; }

        void SetConfig(WebSocketClientConfigModel config);

        Task StartAsync();

        Task ReloadAsync();

        Task StopAsync();

        Task SendCommandAsync(ICommand command);

        Task SendCommandByStringAsync(ICommand command);

        Task SendCommandByBytesAsync(ICommand command);

        Task StartListeningEventAsync();
    }
    //public delegate void ConfigEvent(ConfigEventArgs args);
    //public delegate void ConnectServerEvent(ConnectServerEventArgs args);
    //public delegate void ReceiveEventEvent(ReceiveEventEventArgs args);
    //public delegate void SendCommandEvent(SendCommandEventArgs args);
}
