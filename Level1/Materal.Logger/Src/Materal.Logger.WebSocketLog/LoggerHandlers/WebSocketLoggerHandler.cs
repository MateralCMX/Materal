using Fleck;
using LogLevel = Fleck.LogLevel;

namespace Materal.Logger.LoggerHandlers
{
    /// <summary>
    /// WebSocket日志处理器
    /// </summary>
    public class WebSocketLoggerHandler : LoggerHandler<WebSocketLoggerTargetConfigModel>
    {
        private readonly Dictionary<string, WebSocketServer> _webSocketServers = [];
        private readonly Dictionary<string, List<IWebSocketConnection>> _webSocketConnections = [];
        private readonly Timer _verifyWebSocketServerTimer;
        private const int VerifyWebSocketServerInterval = 5000;
        private readonly object GetWebSocketServerLock = new();
        /// <summary>
        /// 构造方法
        /// </summary>
        public WebSocketLoggerHandler(LoggerRuntime loggerRuntime) : base(loggerRuntime)
        {
            FleckLog.LogAction = WriteWebsocketMessage;
            _verifyWebSocketServerTimer = new(VerifyWebSocketServerTimerElapsed);
        }
        /// <summary>
        /// 写入WebSocket消息
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        private void WriteWebsocketMessage(LogLevel logLevel, string message, Exception? ex)
        {
            const string name = "WebSocketLogger";
            switch (logLevel)
            {
                case LogLevel.Debug:
                    LoggerLog.LogDebug(message, name);
                    break;
                case LogLevel.Info:
                    LoggerLog.LogInfomation(message, name);
                    break;
                case LogLevel.Warn:
                    LoggerLog.LogWarning(message, ex, name);
                    break;
                case LogLevel.Error:
                    LoggerLog.LogError(message, ex, name);
                    break;
            }
        }
        /// <summary>
        /// 运行WebSocket服务
        /// </summary>
        public void RunWebSocketServer() => _verifyWebSocketServerTimer.Change(TimeSpan.Zero, Timeout.InfiniteTimeSpan);
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="target"></param>
        /// <param name="model"></param>
        protected override void Handler(LoggerRuleConfigModel rule, WebSocketLoggerTargetConfigModel target, LoggerHandlerModel model)
        {
            WebSocketServer? webSocketServer = GetWebSocketServer(target);
            if (webSocketServer is null) return;
            if (!_webSocketConnections.ContainsKey(target.Name)) return;
            List<IWebSocketConnection> connections = _webSocketConnections[target.Name];
            for (int i = 0; i < connections.Count; i++)
            {
                IWebSocketConnection connection = connections[i];
                if (!connection.IsAvailable)
                {
                    connections.Remove(connection);
                    i--;
                    continue;
                }
                try
                {
                    connection.Send(new LogMessageModel(model, Config).ToJson());
                }
                catch (Exception ex)
                {
                    LoggerLog.LogError("WebSocket发送日志失败", ex);
                }
            }
        }
        /// <summary>
        /// 获取WebSocket服务
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        private WebSocketServer? GetWebSocketServer(WebSocketLoggerTargetConfigModel target)
        {
            if (!target.Enable) return null;
            WebSocketServer result;
            lock (GetWebSocketServerLock)
            {
                if (_webSocketServers.ContainsKey(target.Name))
                {
                    result = _webSocketServers[target.Name];
                    if (result.Port != target.Port)
                    {
                        result.Dispose();
                        result = InitNewWebSocketServer(target.Name, target.Port);
                        _webSocketServers[target.Name] = result;
                    }
                }
                else
                {
                    result = InitNewWebSocketServer(target.Name, target.Port);
                    _webSocketServers.Add(target.Name, result);
                }
            }
            return result;
        }
        /// <summary>
        /// 初始化一个新的WebSocket服务
        /// </summary>
        /// <param name="name"></param>
        /// <param name="prot"></param>
        /// <returns></returns>
        private WebSocketServer InitNewWebSocketServer(string name, int prot)
        {
            WebSocketServer result = new($"ws://0.0.0.0:{prot}");
            result.Start(connection =>
            {
                connection.OnOpen = () =>
                {
                    LoggerLog.LogDebug($"监测程序已连接:[{name}]{connection.ConnectionInfo.ClientIpAddress}:{connection.ConnectionInfo.ClientPort}");
                    if (!_webSocketConnections.ContainsKey(name))
                    {
                        _webSocketConnections.Add(name, []);
                    }
                    _webSocketConnections[name].Add(connection);
                };
                connection.OnMessage = message =>
                {
                    LoggerLog.LogInfomation($"[{name}]{connection.ConnectionInfo.ClientIpAddress}:{connection.ConnectionInfo.ClientPort}=>{message}");
                };
                connection.OnClose = () =>
                {
                    LoggerLog.LogDebug($"监测程序已关闭:[{name}]{connection.ConnectionInfo.ClientIpAddress}:{connection.ConnectionInfo.ClientPort}");
                    connection.Close();
                    if (!_webSocketConnections.ContainsKey(name)) return;
                    _webSocketConnections[name].Remove(connection);
                };
            });
            return result;
        }
        /// <summary>
        /// 清空定时器执行
        /// </summary>
        /// <param name="stateInfo"></param>
        private void VerifyWebSocketServerTimerElapsed(object? stateInfo)
        {
            List<WebSocketLoggerTargetConfigModel> targets = Config.GetAllTargets<WebSocketLoggerTargetConfigModel>();
            #region 关闭禁用或不存在的服务
            string[] enableTargetNames = targets.Select(m => m.Name).ToArray();
            string[] allKeys = [.. _webSocketServers.Keys];
            foreach (string key in allKeys)
            {
                if (enableTargetNames.Contains(key)) continue;
                if (!_webSocketServers.ContainsKey(key)) continue;
                if (_webSocketConnections.ContainsKey(key))
                {
                    foreach (IWebSocketConnection connection in _webSocketConnections[key])
                    {
                        if (!connection.IsAvailable) continue;
                        connection.Close();
                    }
                    _webSocketConnections.Remove(key);
                }
                _webSocketServers[key].Dispose();
                _webSocketServers.Remove(key);
            }
            #endregion
            #region 开启配置的服务
            foreach (WebSocketLoggerTargetConfigModel target in targets)
            {
                GetWebSocketServer(target);
            }
            #endregion
            if (!IsClose)
            {
                _verifyWebSocketServerTimer.Change(TimeSpan.FromMilliseconds(VerifyWebSocketServerInterval), Timeout.InfiniteTimeSpan);
            }
        }
    }
}
