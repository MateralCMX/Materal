using Fleck;
using System.Collections.Concurrent;

namespace Materal.Logger.WebSocketLogger
{
    /// <summary>
    /// WebSocket日志写入器
    /// </summary>
    public class WebSocketLoggerWriter(IOptionsMonitor<LoggerOptions> options, ILoggerInfo loggerInfo) : BaseLoggerWriter<WebSocketLoggerTargetOptions>(options, loggerInfo)
    {
        private readonly ConcurrentDictionary<int, LoggerWebSocketServer> _webSocketServers = [];
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="log"></param>
        /// <param name="ruleOptions"></param>
        /// <param name="targetOptions"></param>
        /// <returns></returns>
        public override async Task LogAsync(Log log, LoggerRuleOptions ruleOptions, WebSocketLoggerTargetOptions targetOptions)
        {
            LoggerWebSocketServer server = GetLoggerWebSocketServer(targetOptions.Port);
            await server.LogAsync(log);
        }
        /// <summary>
        /// 启动
        /// </summary>
        /// <returns></returns>
        public override async Task StartAsync()
        {
            await base.StartAsync();
            FleckLog.LogAction = (level, message, exception) => { };
            await Task.CompletedTask;
        }
        /// <summary>
        /// 停止
        /// </summary>
        /// <returns></returns>
        public override async Task StopAsync()
        {
            await base.StopAsync();
            foreach (KeyValuePair<int, LoggerWebSocketServer> item in _webSocketServers)
            {
                item.Value.Stop();
            }
        }
        /// <summary>
        /// 获取日志WebSocket服务器
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        private LoggerWebSocketServer GetLoggerWebSocketServer(int port)
        {
            if (_webSocketServers.TryGetValue(port, out LoggerWebSocketServer? result)) return result;
            result = new LoggerWebSocketServer(port, LoggerInfo);
            _webSocketServers.TryAdd(port, result);
            return result;
        }
    }
}
