namespace Materal.Logger.WebSocketLogger
{
    /// <summary>
    /// 自定义日志配置
    /// </summary>
    public static class LoggerOptionsExtensions
    {
        /// <summary>
        /// 添加一个WebSocket目标
        /// </summary>
        /// <param name="options"></param>
        /// <param name="name"></param>
        /// <param name="port"></param>
        public static LoggerOptions AddWebSocketTarget(this LoggerOptions options, string name, int port = 5002)
        {
            foreach (LoggerTargetOptions targetOptions in options.Targets)
            {
                if (targetOptions is not WebSocketLoggerTargetOptions webSocketLoggerTargetOptions) continue;
                if (webSocketLoggerTargetOptions.Port != port) continue;
                return options;
            }
            WebSocketLoggerTargetOptions target = new()
            {
                Name = name,
                Port = port
            };
            options.AddTarget(target);
            return options;
        }
    }
}
