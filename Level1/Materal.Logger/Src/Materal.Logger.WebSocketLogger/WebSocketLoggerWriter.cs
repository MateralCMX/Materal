using Fleck;

namespace Materal.Logger.WebSocketLogger
{
    /// <summary>
    /// WebSocket日志写入器
    /// </summary>
    public class WebSocketLoggerWriter(WebSocketLoggerTargetConfig target) : BaseLoggerWriter<WebSocketLoggerWriterModel, WebSocketLoggerTargetConfig>(target), ILoggerWriter
    {
        private WebSocketServer? _webSocketServer = null;
        private readonly List<IWebSocketConnection> _webSocketConnections = [];
        /// <summary>
        /// 配置变更事件
        /// </summary>
        public override Action<LoggerConfig, IServiceProvider>? OnLoggerConfigChanged => UpdateWebSocketServer;
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override async Task WriteLoggerAsync(WebSocketLoggerWriterModel model)
        {
            if(_webSocketServer is null) return;
            for (int i = 0; i < _webSocketConnections.Count; i++)
            {
                IWebSocketConnection connection = _webSocketConnections[i];
                if (!connection.IsAvailable)
                {
                    _webSocketConnections.Remove(connection);
                    i--;
                    continue;
                }
                try
                {
                    await connection.Send(new LogMessageModel(model).ToJson());
                }
                catch (Exception ex)
                {
                    LoggerHost.LoggerLog?.LogError($"[{Target.Name}]发送日志[{connection.ConnectionInfo.ClientIpAddress}:{connection.ConnectionInfo.ClientPort}]失败", ex);
                }
            }
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <returns></returns>
        public override async Task ShutdownAsync()
        {
            IsClose = true;
            LoggerHost.LoggerLog?.LogDebug($"正在关闭[{Target.Name}]");
            StopWebSocketServer();
            await Task.CompletedTask;
            LoggerHost.LoggerLog?.LogDebug($"[{Target.Name}]关闭成功");
        }
        /// <summary>
        /// 更新WebSocket服务
        /// </summary>
        /// <param name="config"></param>
        /// <param name="serviceProvider"></param>
        public void UpdateWebSocketServer(LoggerConfig config, IServiceProvider serviceProvider)
        {
            if (CanRun(config.Rules))
            {
                if (_webSocketServer is null)
                {
                    StartWebSocketServer();
                }
                else if (_webSocketServer.Port != Target.Port)
                {
                    RestartWebSocketServer();
                }
            }
            else
            {
                StopWebSocketServer();
            }
        }
        /// <summary>
        /// 是否可以运行
        /// </summary>
        /// <param name="rules"></param>
        /// <returns></returns>
        private bool CanRun(List<RuleConfig> rules)
        {
            if (!Target.Enable) return false;
            foreach (RuleConfig rule in rules)
            {
                if (rule.Targets.Contains(Target.Name)) return true;
            }
            return false;
        }
        /// <summary>
        /// 初始化一个新的WebSocket服务
        /// </summary>
        /// <returns></returns>
        private void StartWebSocketServer()
        {
            string url = $"ws://0.0.0.0:{Target.Port}";
            try
            {
                LoggerHost.LoggerLog?.LogDebug($"正在启动WebSocket服务[{url}]");
                _webSocketServer = new(url);
                _webSocketServer.Start(connection =>
                {
                    connection.OnOpen = () =>
                    {
                        LoggerHost.LoggerLog?.LogDebug($"新的[{Target.Name}]客户端[{connection.ConnectionInfo.ClientIpAddress}:{connection.ConnectionInfo.ClientPort}]");
                        _webSocketConnections.Add(connection);
                    };
                    connection.OnMessage = message =>
                    {
                        LoggerHost.LoggerLog?.LogDebug($"[{Target.Name}]客户端[{connection.ConnectionInfo.ClientIpAddress}:{connection.ConnectionInfo.ClientPort}]\r\n{message}");
                    };
                    connection.OnClose = () =>
                    {
                        LoggerHost.LoggerLog?.LogDebug($"[{Target.Name}]客户端[{connection.ConnectionInfo.ClientIpAddress}:{connection.ConnectionInfo.ClientPort}]已断开");
                        connection.Close();
                        if (!_webSocketConnections.Contains(connection)) return;
                        _webSocketConnections.Remove(connection);
                    };
                });
                LoggerHost.LoggerLog?.LogDebug($"WebSocket服务[{url}]启动成功");
            }
            catch (Exception ex)
            {
                _webSocketServer = null;
                LoggerHost.LoggerLog?.LogError($"WebSocket服务[{url}]启动失败", ex);
            }
        }
        /// <summary>
        /// 关闭WebSocket服务
        /// </summary>
        private void StopWebSocketServer()
        {
            if (_webSocketServer is null) return;
            string url = _webSocketServer.Location;
            LoggerHost.LoggerLog?.LogDebug($"正在停止WebSocket服务[{url}]");
            _webSocketServer.Dispose();
            _webSocketServer = null;
            foreach (IWebSocketConnection connection in _webSocketConnections)
            {
                if (!connection.IsAvailable) continue;
                connection.Close();
            }
            _webSocketConnections.Clear();
            LoggerHost.LoggerLog?.LogDebug($"WebSocket服务[{url}]停止成功");
        }
        /// <summary>
        /// 重启WebSocket服务
        /// </summary>
        private void RestartWebSocketServer()
        {
            StopWebSocketServer();
            StartWebSocketServer();
        }
    }
}
