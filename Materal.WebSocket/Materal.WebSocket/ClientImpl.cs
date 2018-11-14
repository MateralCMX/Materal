using Materal.WebSocket.Commands;
using Materal.WebSocket.Model;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Materal.FileHelper;

namespace Materal.WebSocket
{
    public abstract class ClientImpl : IClient
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        protected ClientImpl()
        {
            _cancellationToken = new CancellationToken();
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="config"></param>
        protected ClientImpl(ClientConfigModel config)
        {
            _cancellationToken = new CancellationToken();
            SetConfig(config);
        }

        /// <summary>
        /// 配置对象
        /// </summary>
        private ClientConfigModel _config;

        /// <summary>
        /// 配置对象
        /// </summary>
        public ClientConfigModel Config
        {
            get => _config;
            protected set
            {
                _config = value;
                var args = new ConfigEventArgs
                {
                    Config = _config,
                    Message = _config == null ? "未进行配置" : "" + _config.Url
                };
                OnConfigChange?.Invoke(args);
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
        private ClientStateEnum _state = ClientStateEnum.NotConfigured;

        public ClientStateEnum State
        {
            get => _state;
            private set
            {
                _state = value;
                OnStateChange?.Invoke(new ConnectServerEventArgs
                {
                    State = State
                });
            }
        }

        public event ConfigEvent OnConfigChange;

        public event ConnectServerEvent OnStateChange;

        public event ReceiveEventEvent OnReceiveEvent;

        public event SendCommandEvent OnSendCommand;

        public void SetConfig(ClientConfigModel config)
        {
            if (config.Verification(out List<string> messages))
            {
                Config = config;
                State = ClientStateEnum.Ready;
            }
            else
            {
                throw new ClientException(string.Join(",", messages));
            }
        }

        public void Dispose()
        {
            if (State == ClientStateEnum.Runing)
            {
                _cancellationToken = new CancellationToken();
                StopAsync().Wait(_cancellationToken);
            }
            _cancellationToken = new CancellationToken();
            State = ClientStateEnum.Stop;
        }

        public async Task ReloadAsync()
        {
            switch (State)
            {
                case ClientStateEnum.NotConfigured:
                    throw new ClientException("服务尚未配置");
                case ClientStateEnum.Ready:
                case ClientStateEnum.ConnectionFailed:
                    await OpenWebStockClientAsync();
                    break;
                case ClientStateEnum.Runing:
                    await StopAsync();
                    await OpenWebStockClientAsync();
                    break;
                case ClientStateEnum.Stop:
                    throw new ClientException("服务已经停止,请重新配置");
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
            if (State != ClientStateEnum.Runing) throw new ClientException("服务尚未启动");
            var buffer = new ArraySegment<byte>(command.ByteArrayData);
            await ClientWebSocket.SendAsync(buffer, WebSocketMessageType.Binary, true, _cancellationToken);
            OnSendCommand?.Invoke(new SendCommandEventArgs
            {
                Message = command.Message,
                Command = command
            });
        }

        public virtual async Task SendCommandByStringAsync(ICommand command)
        {
            if (State != ClientStateEnum.Runing) throw new ClientException("服务尚未启动");
            byte[] byteArray = _config.EncodingType.GetBytes(command.StringData);
            var buffer = new ArraySegment<byte>(byteArray);
            await ClientWebSocket.SendAsync(buffer, WebSocketMessageType.Text, true, _cancellationToken);
            OnSendCommand?.Invoke(new SendCommandEventArgs
            {
                Message = command.Message,
                Command = command
            });
        }

        public async Task StartAsync()
        {
            switch (State)
            {
                case ClientStateEnum.NotConfigured:
                    throw new ClientException("服务尚未配置");
                case ClientStateEnum.Ready:
                case ClientStateEnum.ConnectionFailed:
                    await OpenWebStockClientAsync();
                    break;
                case ClientStateEnum.Runing:
                    throw new ClientException("服务已在运行");
                case ClientStateEnum.Stop:
                    throw new ClientException("服务已经停止,请重新配置");
            }
        }

        public virtual async Task StartListeningEventAsync()
        {
            while (State == ClientStateEnum.Runing && ClientWebSocket != null && ClientWebSocket.State == WebSocketState.Open)
            {
                try
                {
                    var serverByteArray = new byte[_config.ServerMessageMaxLength];
                    var buffer = new ArraySegment<byte>(serverByteArray);
                    WebSocketReceiveResult wsdata = await ClientWebSocket.ReceiveAsync(buffer, _cancellationToken);
                    var bRec = new byte[wsdata.Count];
                    Array.Copy(serverByteArray, bRec, wsdata.Count);
                    OnReceiveEvent?.Invoke(new ReceiveEventEventArgs
                    {
                        ByteArrayData = bRec
                    });
                }
                catch (Exception ex)
                {
                    switch (ClientWebSocket.State)
                    {
                        case WebSocketState.Aborted:
                        case WebSocketState.CloseSent:
                        case WebSocketState.CloseReceived:
                        case WebSocketState.Closed:
                            State = ClientStateEnum.ConnectionFailed;
                            break;
                        case WebSocketState.Connecting:
                        case WebSocketState.None:
                        case WebSocketState.Open:
                            break;
                    }
                    throw new ClientException(ex.Message, ex);
                }
            }
        }

        public virtual async Task StopAsync()
        {
            if (State == ClientStateEnum.Runing)
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
                        throw new ClientException("已发送关闭请求，但服务器没有回应", ex);
                    }
                    finally
                    {
                        ClientWebSocket = null;
                    }
                }
            }
        }

        /// <summary>
        /// 打开WebStock客户端
        /// </summary>
        /// <returns></returns>
        private async Task OpenWebStockClientAsync()
        {
            ClientWebSocket = new ClientWebSocket();
            var uri = new Uri(_config.Url);
            try
            {
                await ClientWebSocket.ConnectAsync(uri, _cancellationToken);
                State = ClientStateEnum.Runing;
            }
            catch (Exception)
            {
                State = ClientStateEnum.ConnectionFailed;
            }
        }
    }
}
