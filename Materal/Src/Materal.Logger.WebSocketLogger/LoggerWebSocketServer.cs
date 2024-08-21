using Fleck;
using Materal.Extensions;

namespace Materal.Logger.WebSocketLogger
{
    /// <summary>
    /// 日志记录器WebSocket服务器
    /// </summary>
    public class LoggerWebSocketServer
    {
        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; }
        private WebSocketServer? _webSocketServer;
        private readonly List<IWebSocketConnection> _connections = [];
        private readonly SemaphoreSlim _webSocketSemaphore = new(0, 1);
        private readonly ILoggerInfo _loggerInfo;
        /// <summary>
        /// 构造方法
        /// </summary>
        public LoggerWebSocketServer(int port, ILoggerInfo loggerInfo)
        {
            _loggerInfo = loggerInfo;
            Port = port;
            _webSocketSemaphore.Release();
        }
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public async Task LogAsync(Log log)
        {
            Run();
            foreach (IWebSocketConnection connection in _connections)
            {
                await connection.Send(log.ToJson());
            }
        }
        /// <summary>
        /// 启动
        /// </summary>
        /// <returns></returns>
        private void Run()
        {
            if (_webSocketServer is not null) return;
            _webSocketSemaphore.Wait();
            if (_webSocketServer is not null) return;
            try
            {
                _webSocketServer = new($"ws://0.0.0.0:{Port}");
                _webSocketServer.Start(connection =>
                {
                    connection.OnOpen = () => OnOpen(connection);
                    connection.OnClose = () => OnClose(connection);
                    connection.OnError = (exception) => OnError(exception, connection);
                });
                _loggerInfo.LogDebug($"WebSocket日志服务器启动:ws://0.0.0.0:{Port}");
            }
            finally
            {
                _webSocketSemaphore.Release();
            }
        }
        /// <summary>
        /// 停止
        /// </summary>
        /// <returns></returns>
        public void Stop()
        {
            if (_webSocketServer is null) return;
            _webSocketSemaphore.Wait();
            if (_webSocketServer is null) return;
            try
            {
                foreach (IWebSocketConnection connection in _connections)
                {
                    connection.Close();
                }
                _connections.Clear();
                _webSocketServer.ListenerSocket.Close();
                _webSocketServer.Dispose();
                _webSocketServer = null;
                _loggerInfo.LogDebug($"WebSocket日志服务器关闭:ws://0.0.0.0:{Port}");
            }
            finally
            {
                _webSocketSemaphore.Release();
            }
        }
        /// <summary>
        /// 链接打开
        /// </summary>
        /// <param name="connection"></param>
        private void OnOpen(IWebSocketConnection connection)
        {
            _loggerInfo.LogDebug($"新链接:{connection.ConnectionInfo.ClientIpAddress}");
            _connections.Add(connection);
        }
        /// <summary>
        /// 链接错误
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="connection"></param>
        private void OnError(Exception exception, IWebSocketConnection connection)
        {
            _loggerInfo.LogError($"链接错误:{connection.ConnectionInfo.ClientIpAddress}", exception);
        }
        /// <summary>
        /// 链接关闭
        /// </summary>
        /// <param name="connection"></param>
        private void OnClose(IWebSocketConnection connection)
        {
            _loggerInfo.LogDebug($"链接关闭:{connection.ConnectionInfo.ClientIpAddress}");
            _connections.Remove(connection);
            if (_connections.Count > 0) return;
            Stop();
        }
    }
}
