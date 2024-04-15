namespace Materal.Logger.WebSocketLogger
{
    /// <summary>
    /// LoggerConfig扩展
    /// </summary>
    public static partial class LoggerConfigExtensions
    {
        /// <summary>
        /// 添加一个WebSocket输出
        /// </summary>
        /// <param name="loggerConfig"></param>
        /// <param name="name"></param>
        /// <param name="port"></param>
        public static LoggerConfig AddWebSocketTarget(this LoggerConfig loggerConfig, string name, int port = 5002)
        {
            WebSocketLoggerTargetConfig target = new()
            {
                Name = name,
                Port = port
            };
            loggerConfig.AddTarget(target);
            return loggerConfig;
        }
    }
}
