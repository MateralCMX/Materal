using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Materal.ConvertHelper;
using Materal.WebSocket.Client.Model;
using Materal.WebSocket.Commands;
using Materal.WebSocket.Events;
using Materal.WebSocket.Model;

namespace Materal.WebSocket.Client
{
    public abstract class WebSocketClientImpl : IWebSocketClient
    {
        public virtual async Task SendCommandByBytesAsync(ICommand command)
        {
            if (ClientWebSocket.State != WebSocketState.Open) throw new MateralWebSocketClientException("服务尚未启动");
            var buffer = new ArraySegment<byte>(command.ToBytes());
            await ClientWebSocket.SendAsync(buffer, WebSocketMessageType.Binary, true, _cancellationToken);
            _clientHandler.OnSendMessage(command.ToJson());
        }

        public virtual async Task SendCommandByStringAsync(ICommand command)
        {
            if (ClientWebSocket.State != WebSocketState.Open) throw new MateralWebSocketClientException("服务尚未启动");
            if (!(Config is WebSocketClientConfig clientConfig)) throw new MateralWebSocketClientException("Config类型必须派生于WebSocketClientConfig");
            string commandJson = command.ToJson();
            byte[] byteArray = clientConfig.EncodingType.GetBytes(commandJson);
            var buffer = new ArraySegment<byte>(byteArray);
            await ClientWebSocket.SendAsync(buffer, WebSocketMessageType.Text, true, _cancellationToken);
            _clientHandler.OnSendMessage(commandJson);
        }
        public IWebSocketClientConfig Config { get; private set; }

        public WebSocketState WebSocketState => ClientWebSocket?.State ?? WebSocketState.None;

        /// <summary>
        /// WebSocket客户端
        /// </summary>
        protected ClientWebSocket ClientWebSocket;

        /// <summary>
        /// 取消标记
        /// </summary>
        private CancellationToken _cancellationToken;

        public void SetConfig(IWebSocketClientConfig config)
        {
            if (config is WebSocketClientConfig webSocketClientConfig)
            {
                if (webSocketClientConfig.Verification(out List<string> messages))
                {
                    Config = webSocketClientConfig;
                }
                else
                {
                    throw new MateralWebSocketClientException(string.Join(",", messages));
                }
            }
            else
            {
                throw new MateralWebSocketClientException("Config类型必须派生于WebSocketClientConfig");
            }
        }
        private WebSocketClientHandler _clientHandler;
        public async Task StartAsync<T>() where T : IWebSocketClientHandler
        {
            var handler = ConvertManager.GetDefaultObject<T>();
            if (!(handler is WebSocketClientHandler clientHandler)) throw new MateralWebSocketClientException("Handler类型必须派生于WebSocketClientHandler");
            _clientHandler = clientHandler;
            await StartAsync();
        }

        public async Task StartAsync()
        {
            try
            {
                if (_clientHandler == null) throw new MateralWebSocketClientException("未设置Handler");
                await OpenWebSocketClientAsync();
                await StartListeningAsync();
            }
            catch (Exception ex)
            {
                _clientHandler?.ExceptionCaught(this, ex);
            }
        }

        public async Task StopAsync()
        {
            if (ClientWebSocket.State == WebSocketState.Connecting || ClientWebSocket.State == WebSocketState.Open)
            {
                try
                {
                    await ClientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "NomalClosure",
                        _cancellationToken);
                }
                catch (Exception ex)
                {
                    throw new MateralWebSocketClientException("已发送关闭请求，但服务器没有回应", ex);
                }
                finally
                {
                    ClientWebSocket = null;
                }
            }
        }
        public abstract Task HandleEventAsync(IEvent eventM);
        public abstract void HandleEvent(IEvent eventM);

        /// <summary>
        /// 打开WebSocket客户端
        /// </summary>
        /// <returns></returns>
        private async Task OpenWebSocketClientAsync()
        {
            if (!(Config is WebSocketClientConfig clientConfig)) throw new MateralWebSocketClientException("Config类型必须派生于WebSocketClientConfig");
            ClientWebSocket = new ClientWebSocket();
            var uri = new Uri(clientConfig.Url);
            try
            {
                _clientHandler.ChannelStart(this);
                _cancellationToken = new CancellationToken();
                await ClientWebSocket.ConnectAsync(uri, _cancellationToken);
                _clientHandler.ChannelActive(this);
            }
            catch(Exception ex)
            {
                _clientHandler?.ExceptionCaught(this, new MateralWebSocketClientException("连接服务器失败", ex));
            }
        }

        /// <summary>
        /// 开始监听
        /// </summary>
        /// <returns></returns>
        private async Task StartListeningAsync()
        {
            if (!(Config is WebSocketClientConfig clientConfig)) throw new MateralWebSocketClientException("Config类型必须派生于WebSocketClientConfig");
            while (ClientWebSocket.State == WebSocketState.Open)
            {
                try
                {
                    var serverByteArray = new byte[clientConfig.ServerMessageMaxLength];
                    var buffer = new ArraySegment<byte>(serverByteArray);
                    WebSocketReceiveResult wsdata = await ClientWebSocket.ReceiveAsync(buffer, _cancellationToken);
                    var bRec = new byte[wsdata.Count];
                    Array.Copy(serverByteArray, bRec, wsdata.Count);
                    _clientHandler?.ChannelRead0(this, bRec);
                }
                catch (Exception ex)
                {
                    _clientHandler?.ExceptionCaught(this, ex);
                }
            }
            _clientHandler?.ChannelInactive(this);
        }
    }
}
