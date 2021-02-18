using Materal.ConvertHelper;
using Materal.DotNetty.CommandBus;
using Materal.DotNetty.Common;
using Materal.DotNetty.EventBus;
using Materal.WebSocketClient.Core;
using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Materal.Common;

namespace Materal.WebSocketClient.CoreImpl
{
    public class WebSocketClientImpl : IWebSocketClient
    {
        private readonly ClientConfig _clientConfig;
        private readonly IEventBus _eventBus;
        private bool _isManualClose;
        private bool _isListening;
        private int _reconnectionNumber;

        public WebSocketClientImpl(ClientConfig clientConfig, IEventBus eventBus)
        {
            _clientConfig = clientConfig;
            _eventBus = eventBus;
        }

        public event Action OnConnectionSuccess;
        public event Action OnConnectionFail;
        public event Action OnClose;
        public event Action<string> OnEventMessage;
        public event Action<string, string> OnSubMessage;
        public event Action<Exception> OnException;
        public WebSocketState State => ClientWebSocket?.State ?? WebSocketState.None;
        protected ClientWebSocket ClientWebSocket;
        /// <summary>
        /// 取消标记
        /// </summary>
        private CancellationToken _cancellationToken;
        public virtual async Task RunAsync()
        {
            if (ClientWebSocket != null) return;
            await StartAsync();
        }

        public async Task ReconnectionAsync()
        {
            if (_clientConfig.ReconnectionNumber > 0 && _reconnectionNumber >= _clientConfig.ReconnectionNumber)
            {
                var exception = new MateralDotNettyException($"已连接失败{_reconnectionNumber}次，请联系管理员。");
                OnException?.Invoke(exception);
                await StopAsync();
                throw exception;
            }
            if (_clientConfig.ReconnectionInterval < 1000)
            {
                throw new MateralDotNettyException("至少等待1000ms");
            }
            if (_isManualClose) return;
            OnSubMessage?.Invoke($"[{_reconnectionNumber + 1}]WebSocketClient将在{_clientConfig.ReconnectionInterval / 1000f:N2}秒后重新连接....", "重要");
            await Task.Delay(_clientConfig.ReconnectionInterval, _cancellationToken);
            _reconnectionNumber++;
            await StartAsync();
        }

        public async Task StopAsync()
        {
            await StopAsync(true);
        }
        public async Task SendCommandAsync(ICommand command)
        {
            if (ClientWebSocket.State != WebSocketState.Open)
            {
                var exception = new MateralDotNettyException("未与服务器连接，不能发送命令");
                OnException?.Invoke(exception);
                return;
            }
            string commandJson = command.ToJson();
            byte[] byteArray = _clientConfig.EncodingType.GetBytes(commandJson);
            var buffer = new ArraySegment<byte>(byteArray);
            await ClientWebSocket.SendAsync(buffer, WebSocketMessageType.Text, true, _cancellationToken);
        }

        private async Task StartAsync()
        {
            try
            {
                OnSubMessage?.Invoke($"正在连接{_clientConfig.Url}....", "重要");
                ClientWebSocket = new ClientWebSocket();
                var uri = new Uri(_clientConfig.Url);
                _cancellationToken = new CancellationToken();
                await ClientWebSocket.ConnectAsync(uri, _cancellationToken);
                OnSubMessage?.Invoke("连接成功", "重要");
                if (!_isListening)
                {
                    StartListeningAsync();
                }
                _isManualClose = false;
                OnConnectionSuccess?.Invoke();
                _reconnectionNumber = 0;
            }
            catch (Exception exception)
            {
                await StopAsync(false);
                OnSubMessage?.Invoke("连接失败", "重要");
                OnException?.Invoke(exception);
                OnConnectionFail?.Invoke();
            }
        }
        private async Task StopAsync(bool isManualClose)
        {
            _isManualClose = isManualClose;
            if (ClientWebSocket == null) return;
            if (ClientWebSocket.State == WebSocketState.Connecting || ClientWebSocket.State == WebSocketState.Open)
            {
                try
                {
                    await ClientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "NormalClosure", _cancellationToken);
                }
                catch (Exception ex)
                {
                    throw new MateralDotNettyException("已发送关闭请求，但服务器没有回应", ex);
                }
            }
            ClientWebSocket = null;
        }
        /// <summary>
        /// 开始监听
        /// </summary>
        /// <returns></returns>
        private async void StartListeningAsync()
        {
            if (_isListening) return;
            _isListening = true;
            while (ClientWebSocket.State == WebSocketState.Open)
            {
                byte[] serverByteArray = new byte[655300000];
                ArraySegment<byte> buffer = new ArraySegment<byte>(serverByteArray);
                WebSocketReceiveResult wsData;
                try
                {
                    wsData = await ClientWebSocket.ReceiveAsync(buffer, _cancellationToken);
                    if (wsData == null)
                    {
                        MateralDotNettyException materalDotNettyException = new MateralDotNettyException("未获取到服务器数据");
                        OnException?.Invoke(materalDotNettyException);
                    }
                }
                catch (Exception exception)
                {
                    if (_isManualClose) return;
                    await StopAsync(false);
                    OnSubMessage?.Invoke("与服务器断开连接", "重要");
                    OnException?.Invoke(exception);
                    OnClose?.Invoke();
                    _isListening = false;
                    continue;
                }
                try
                {
                    string eventJson = GetEventJson(serverByteArray, wsData);
                    try
                    {
                        await HandlerMessageAsync(eventJson);
                    }
                    catch (Exception exception)
                    {
                        OnSubMessage?.Invoke("处理事件出现错误", "重要");
                        OnSubMessage?.Invoke(eventJson, "重要");
                        OnException?.Invoke(exception);
                    }
                }
                catch (Exception exception)
                {
                    OnSubMessage?.Invoke("处理事件出现错误", "重要");
                    OnSubMessage?.Invoke("事件数据反序列化失败", "重要");
                    OnException?.Invoke(exception);
                }
            }
            _isListening = false;
        }
        /// <summary>
        /// 获得事件Json
        /// </summary>
        /// <param name="serverByteArray"></param>
        /// <param name="wsData"></param>
        /// <returns></returns>
        private string GetEventJson(byte[] serverByteArray, WebSocketReceiveResult wsData)
        {
            byte[] bRec = new byte[wsData.Count];
            Array.Copy(serverByteArray, bRec, wsData.Count);
            string eventJson = _clientConfig.EncodingType.GetString(bRec);
            return eventJson;
        }
        /// <summary>
        /// 处理消息
        /// </summary>
        /// <param name="eventJson"></param>
        /// <returns></returns>
        private async Task HandlerMessageAsync(string eventJson)
        {
            OnEventMessage?.Invoke(eventJson);
            IEvent @event = eventJson.JsonToObject<BaseEvent>();
            IEventHandler eventHandler = _eventBus.GetEventHandler(@event.EventHandler);
            @event = (IEvent)eventJson.JsonToObject(eventHandler.EventType);
            await eventHandler.HandlerAsync(@event, ClientWebSocket);
        }
    }
}
