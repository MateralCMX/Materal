namespace Materal.Logger
{
    /// <summary>
    /// 日志配置选项扩展
    /// </summary>
    public static class LoggerConfigOptionsExtension
    {
        /// <summary>
        /// 添加一个WebSocket输出
        /// </summary>
        /// <param name="loggerConfigOptions"></param>
        /// <param name="name"></param>
        /// <param name="port"></param>
        public static LoggerConfigOptions AddWebSocketTarget(this LoggerConfigOptions loggerConfigOptions, string name, int port = 5002)
        {
            WebSocketLoggerTargetConfigModel target = new()
            {
                Name = name,
                Port = port
            };
            loggerConfigOptions.AddTarget(target);
            return loggerConfigOptions;
        }
    }
}
