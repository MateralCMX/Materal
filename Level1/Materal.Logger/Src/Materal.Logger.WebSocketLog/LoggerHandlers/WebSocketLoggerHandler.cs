using Fleck;
using Materal.Logger.LoggerHandlers.Models;
using Materal.Logger.Models;
using Materal.Logger.WebSocketLog.LoggerHandlers.Models;

namespace Materal.Logger.LoggerHandlers
{
    /// <summary>
    /// WebSocket日志处理器
    /// </summary>
    public class WebSocketLoggerHandler : LoggerHandler
    {
        private static readonly Dictionary<string, WebSocketServer> _webSocketServers = new();
        private static readonly Dictionary<string, List<IWebSocketConnection>> _webSocketConnections = new();
        private static Timer _verifyWebSocketServerTimer = new(VerifyWebSocketServerTimerElapsed);
        private const int VerifyWebSocketServerInterval = 5000;
        /// <summary>
        /// 静态构造方法
        /// </summary>
        static WebSocketLoggerHandler()
        {
            FleckLog.LogAction = WriteWebsocketMessage;
            LoggerTargetConfigModel[] targets = AllTargets.Where(m => m.Enable && m.Type == "WebSocket").ToArray();
            foreach (LoggerTargetConfigModel target in targets)
            {
                WebSocketServer? webSocketServer = GetWebSocketServer(target);
                if (webSocketServer is null) return;
            }
            _verifyWebSocketServerTimer.Change(TimeSpan.FromMilliseconds(VerifyWebSocketServerInterval), Timeout.InfiniteTimeSpan);
        }
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="target"></param>
        /// <param name="model"></param>
        protected override void Handler(LoggerRuleConfigModel rule, LoggerTargetConfigModel target, LoggerHandlerModel model)
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
                    connection.Send(new LogMessageModel(target, model).ToJson());
                }
                catch (Exception ex)
                {
                    LoggerLog.LogWarning("WebSocket发送消息失败", ex);
                }
            }
        }
        /// <summary>
        /// 获取WebSocket服务
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        private static WebSocketServer? GetWebSocketServer(LoggerTargetConfigModel target)
        {
            if (!target.Enable) return null;
            if (target.Port is null) return null;
            WebSocketServer result;
            if (_webSocketServers.ContainsKey(target.Name))
            {
                result = _webSocketServers[target.Name];
                if (result.Port != target.Port.Value)
                {
                    result.Dispose();
                    result = InitNewWebSocketServer(target.Name, target.Port.Value);
                    _webSocketServers[target.Name] = result;
                }
            }
            else
            {
                if (target.Port is null) return null;
                result = InitNewWebSocketServer(target.Name, target.Port.Value);
                _webSocketServers.Add(target.Name, result);
            }
            return result;
        }
        /// <summary>
        /// 初始化一个新的WebSocket服务
        /// </summary>
        /// <param name="name"></param>
        /// <param name="prot"></param>
        /// <returns></returns>
        private static WebSocketServer InitNewWebSocketServer(string name, int prot)
        {
            WebSocketServer result = new($"ws://0.0.0.0:{prot}");
            result.Start(connection =>
            {
                connection.OnOpen = () =>
                {
                    LoggerLog.LogDebug($"监测程序已连接:[{name}]{connection.ConnectionInfo.ClientIpAddress}:{connection.ConnectionInfo.ClientPort}");
                    if (!_webSocketConnections.ContainsKey(name))
                    {
                        _webSocketConnections.Add(name, new());
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
        /// 写入WebSocket消息
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        private static void WriteWebsocketMessage(LogLevel logLevel, string message, Exception? ex)
        {
            string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}|{logLevel}|WebSocketLoggerHandler]{message}";
            switch (logLevel)
            {
                case LogLevel.Debug:
                    LoggerLog.LogDebug(logMessage);
                    break;
                case LogLevel.Info:
                    LoggerLog.LogInfomation(logMessage);
                    break;
                case LogLevel.Warn:
                    LoggerLog.LogWarning(logMessage, ex);
                    break;
                case LogLevel.Error:
                    LoggerLog.LogError(logMessage, ex);
                    break;
            }
        }
        /// <summary>
        /// 清空定时器执行
        /// </summary>
        /// <param name="stateInfo"></param>
        public static void VerifyWebSocketServerTimerElapsed(object? stateInfo)
        {
            LoggerTargetConfigModel[] targets = AllTargets.Where(m => m.Enable && m.Type == "WebSocket").ToArray();
            #region 关闭禁用或不存在的服务
            string[] enableTargetNames = targets.Select(m => m.Name).ToArray();
            string[] allKeys = _webSocketServers.Keys.ToArray();
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
            foreach (LoggerTargetConfigModel target in targets)
            {
                WebSocketServer? webSocketServer = GetWebSocketServer(target);
                if (webSocketServer is null) return;
            }
            if (!Logger.IsClose)
            {
                _verifyWebSocketServerTimer.Change(TimeSpan.FromMilliseconds(VerifyWebSocketServerInterval), Timeout.InfiniteTimeSpan);
            }
        }
    }
}
