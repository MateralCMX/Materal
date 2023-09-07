﻿using Fleck;
using Materal.Logger.LoggerHandlers.Models;
using Materal.Logger.Models;
using Materal.Logger.WebSocketLog.LoggerHandlers.Models;

namespace Materal.Logger.LoggerHandlers
{
    /// <summary>
    /// WebSocket日志处理器
    /// </summary>
    public class WebSocketLoggerHandler : LoggerHandler<WebSocketLoggerTargetConfigModel>
    {
        private static readonly Dictionary<string, WebSocketServer> _webSocketServers = new();
        private static readonly Dictionary<string, List<IWebSocketConnection>> _webSocketConnections = new();
        private static readonly Timer _verifyWebSocketServerTimer = new(VerifyWebSocketServerTimerElapsed);
        private const int VerifyWebSocketServerInterval = 5000;
        private static readonly object GetWebSocketServerLock = new();
        /// <summary>
        /// 静态构造方法
        /// </summary>
        static WebSocketLoggerHandler()
        {
            FleckLog.LogAction = WriteWebsocketMessage;
        }
        /// <summary>
        /// 运行WebSocket服务
        /// </summary>
        public static void RunWebSocketServer()
        {
            _verifyWebSocketServerTimer.Change(TimeSpan.Zero, Timeout.InfiniteTimeSpan);
        }
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
        private static WebSocketServer? GetWebSocketServer(WebSocketLoggerTargetConfigModel target)
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
            WebSocketLoggerTargetConfigModel[] targets = AllTargets.ToArray();
            #region 关闭禁用或不存在的服务
            string[] enableTargetNames = AllTargets.Select(m => m.Name).ToArray();
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
            #region 开启配置的服务
            foreach (WebSocketLoggerTargetConfigModel target in targets)
            {
                GetWebSocketServer(target);
            }
            #endregion
            if (!Logger.IsClose)
            {
                _verifyWebSocketServerTimer.Change(TimeSpan.FromMilliseconds(VerifyWebSocketServerInterval), Timeout.InfiniteTimeSpan);
            }
        }
    }
}
