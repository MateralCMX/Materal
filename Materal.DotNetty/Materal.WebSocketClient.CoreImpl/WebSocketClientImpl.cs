using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Materal.ConvertHelper;
using Materal.DotNetty.CommandBus;
using Materal.DotNetty.Common;
using Materal.DotNetty.EventBus;
using Materal.WebSocketClient.Core;

namespace Materal.WebSocketClient.CoreImpl
{
    public class WebSocketClientImpl : IWebSocketClient
    {
        private readonly ClientConfig _clientConfig;
        private readonly IEventBus _eventBus;

        public WebSocketClientImpl(ClientConfig clientConfig, IEventBus eventBus)
        {
            _clientConfig = clientConfig;
            _eventBus = eventBus;
        }

        public event Action OnConnectionSuccess;
        public event Action OnConnectionFail;
        public event Action OnClose;
        public event Action<string, string> OnSubMessage;
        public event Action<Exception> OnException;
        protected ClientWebSocket ClientWebSocket;
        /// <summary>
        /// 取消标记
        /// </summary>
        private CancellationToken _cancellationToken;
        public virtual async Task RunAsync()
        {
            try
            {
                OnSubMessage?.Invoke($"正在连接{_clientConfig.Url}....", "重要");
                ClientWebSocket = new ClientWebSocket();
                var uri = new Uri(_clientConfig.Url);
                _cancellationToken = new CancellationToken();
                await ClientWebSocket.ConnectAsync(uri, _cancellationToken);
                OnSubMessage?.Invoke("连接成功", "重要");
                StartListeningAsync();
                OnConnectionSuccess?.Invoke();
            }
            catch (Exception exception)
            {
                OnSubMessage?.Invoke("连接失败", "重要");
                OnException?.Invoke(exception);
                OnConnectionFail?.Invoke();
            }
        }

        public async Task StopAsync()
        {
            if (ClientWebSocket.State == WebSocketState.Connecting || ClientWebSocket.State == WebSocketState.Open)
            {
                try
                {
                    await ClientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "NormalClosure",
                        _cancellationToken);
                }
                catch (Exception ex)
                {
                    throw new DotNettyException("已发送关闭请求，但服务器没有回应", ex);
                }
                finally
                {
                    ClientWebSocket = null;
                }
            }
        }
        public async Task SendCommandAsync(ICommand command)
        {
            if (ClientWebSocket.State != WebSocketState.Open)
            {
                var exception = new DotNettyException("未与服务器连接，不能发送命令");
                OnException?.Invoke(exception);
                return;
            }
            string commandJson = command.ToJson();
            byte[] byteArray = _clientConfig.EncodingType.GetBytes(commandJson);
            var buffer = new ArraySegment<byte>(byteArray);
            await ClientWebSocket.SendAsync(buffer, WebSocketMessageType.Text, true, _cancellationToken);
        }
        /// <summary>
        /// 开始监听
        /// </summary>
        /// <returns></returns>
        private async void StartListeningAsync()
        {
            try
            {
                while (ClientWebSocket.State == WebSocketState.Open)
                {
                    var serverByteArray = new byte[655300000];
                    var buffer = new ArraySegment<byte>(serverByteArray);
                    WebSocketReceiveResult wsData = await ClientWebSocket.ReceiveAsync(buffer, _cancellationToken);
                    var bRec = new byte[wsData.Count];
                    Array.Copy(serverByteArray, bRec, wsData.Count);
                    string eventJson = _clientConfig.EncodingType.GetString(bRec);
                    IEvent @event = eventJson.JsonToObject<BaseEvent>();
                    IEventHandler eventHandler = _eventBus.GetEventHandler(@event.EventHandler);
                    @event = (IEvent)eventJson.JsonToObject(eventHandler.EventType);
                    await eventHandler.HandlerAsync(@event, ClientWebSocket);
                }
            }
            catch (Exception exception)
            {
                await StopAsync();
                OnSubMessage?.Invoke("与服务器断开连接", "重要");
                OnException?.Invoke(exception);
                OnClose?.Invoke();
            }
        }
    }
}
