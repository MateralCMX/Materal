using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Materal.WebSocket.Client.Model;
using Materal.WebSocket.Commands;
using Materal.WebSocket.Model;

namespace Materal.WebSocket.Client
{
    public abstract class WebSocketClientImpl : IWebSocketClient
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        protected WebSocketClientImpl()
        {
            _cancellationToken = new CancellationToken();
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="config"></param>
        protected WebSocketClientImpl(WebSocketClientConfigModel config)
        {
            _cancellationToken = new CancellationToken();
            SetConfig(config);
        }

        /// <summary>
        /// 配置对象
        /// </summary>
        private WebSocketClientConfigModel _config;

        /// <summary>
        /// 配置对象
        /// </summary>
        public WebSocketClientConfigModel Config
        {
            get => _config;
            protected set
            {
                _config = value;
                //var args = new ConfigEventArgs
                //{
                //    Config = _config,
                //    Message = _config == null ? "未进行配置" : "" + _config.Url
                //};
                //OnConfigChange?.Invoke(args);
            }
        }

        /// <summary>
        /// WebSocket客户端
        /// </summary>
        protected ClientWebSocket ClientWebSocket;

        /// <summary>
        /// 取消标记
        /// </summary>
        private CancellationToken _cancellationToken;

        /// <summary>
        /// 客户端状态
        /// </summary>
        private WebSocketClientStateEnum _state = WebSocketClientStateEnum.NotConfigured;

        public WebSocketClientStateEnum State
        {
            get => _state;
            private set
            {
                _state = value;
                //OnStateChange?.Invoke(new ConnectServerEventArgs
                //{
                //    State = State
                //});
            }
        }

        //public event ConfigEvent OnConfigChange;

        //public event ConnectServerEvent OnStateChange;

        //public event ReceiveEventEvent OnReceiveEvent;

        //public event SendCommandEvent OnSendCommand;

        public void SetConfig(WebSocketClientConfigModel config)
        {
            if (config.Verification(out List<string> messages))
            {
                Config = config;
                State = WebSocketClientStateEnum.Ready;
            }
            else
            {
                throw new MateralWebSocketClientException(string.Join(",", messages));
            }
        }

        public void Dispose()
        {
            if (State == WebSocketClientStateEnum.Runing)
            {
                _cancellationToken = new CancellationToken();
                StopAsync().Wait(_cancellationToken);
            }
            _cancellationToken = new CancellationToken();
            State = WebSocketClientStateEnum.Stop;
        }

        public async Task ReloadAsync()
        {
            switch (State)
            {
                case WebSocketClientStateEnum.NotConfigured:
                    throw new MateralWebSocketClientException("服务尚未配置");
                case WebSocketClientStateEnum.Ready:
                case WebSocketClientStateEnum.ConnectionFailed:
                    await OpenWebSocketClientAsync();
                    break;
                case WebSocketClientStateEnum.Runing:
                    await StopAsync();
                    await OpenWebSocketClientAsync();
                    break;
                case WebSocketClientStateEnum.Stop:
                    throw new MateralWebSocketClientException("服务已经停止,请重新配置");
            }
        }

        public async Task SendCommandAsync(ICommand command)
        {
            if (command.ByteArrayData != null && command.ByteArrayData.Length > 0)
            {
                await SendCommandByBytesAsync(command);
            }
            else
            {
                await SendCommandByStringAsync(command);
            }
        }

        public virtual async Task SendCommandByBytesAsync(ICommand command)
        {
            if (State != WebSocketClientStateEnum.Runing) throw new MateralWebSocketClientException("服务尚未启动");
            var buffer = new ArraySegment<byte>(command.ByteArrayData);
            await ClientWebSocket.SendAsync(buffer, WebSocketMessageType.Binary, true, _cancellationToken);
            //OnSendCommand?.Invoke(new SendCommandEventArgs
            //{
            //    Message = command.Message,
            //    Command = command
            //});
        }

        public virtual async Task SendCommandByStringAsync(ICommand command)
        {
            if (State != WebSocketClientStateEnum.Runing) throw new MateralWebSocketClientException("服务尚未启动");
            byte[] byteArray = _config.EncodingType.GetBytes(command.StringData);
            var buffer = new ArraySegment<byte>(byteArray);
            await ClientWebSocket.SendAsync(buffer, WebSocketMessageType.Text, true, _cancellationToken);
            //OnSendCommand?.Invoke(new SendCommandEventArgs
            //{
            //    Message = command.Message,
            //    Command = command
            //});
        }

        public async Task StartAsync()
        {
            switch (State)
            {
                case WebSocketClientStateEnum.NotConfigured:
                    throw new MateralWebSocketClientException("服务尚未配置");
                case WebSocketClientStateEnum.Ready:
                case WebSocketClientStateEnum.ConnectionFailed:
                    await OpenWebSocketClientAsync();
                    break;
                case WebSocketClientStateEnum.Runing:
                    throw new MateralWebSocketClientException("服务已在运行");
                case WebSocketClientStateEnum.Stop:
                    throw new MateralWebSocketClientException("服务已经停止,请重新配置");
            }
        }

        public virtual async Task StartListeningEventAsync()
        {
            while (State == WebSocketClientStateEnum.Runing && ClientWebSocket != null && ClientWebSocket.State == WebSocketState.Open)
            {
                try
                {
                    var serverByteArray = new byte[_config.ServerMessageMaxLength];
                    var buffer = new ArraySegment<byte>(serverByteArray);
                    WebSocketReceiveResult wsdata = await ClientWebSocket.ReceiveAsync(buffer, _cancellationToken);
                    var bRec = new byte[wsdata.Count];
                    Array.Copy(serverByteArray, bRec, wsdata.Count);
                    //OnReceiveEvent?.Invoke(new ReceiveEventEventArgs
                    //{
                    //    ByteArrayData = bRec
                    //});
                }
                catch (Exception ex)
                {
                    switch (ClientWebSocket.State)
                    {
                        case WebSocketState.Aborted:
                        case WebSocketState.CloseSent:
                        case WebSocketState.CloseReceived:
                        case WebSocketState.Closed:
                            State = WebSocketClientStateEnum.ConnectionFailed;
                            break;
                    }
                    throw new MateralWebSocketClientException(ex.Message, ex);
                }
            }
        }

        public virtual async Task StopAsync()
        {
            if (State == WebSocketClientStateEnum.Runing)
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
        }

        /// <summary>
        /// 打开WebSocket客户端
        /// </summary>
        /// <returns></returns>
        private async Task OpenWebSocketClientAsync()
        {
            ClientWebSocket = new ClientWebSocket();
            var uri = new Uri(_config.Url);
            try
            {
                await ClientWebSocket.ConnectAsync(uri, _cancellationToken);
                State = WebSocketClientStateEnum.Runing;
            }
            catch (Exception)
            {
                State = WebSocketClientStateEnum.ConnectionFailed;
            }
        }
    }
}
