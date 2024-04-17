namespace Materal.Logger.WebSocketLogger
{
    /// <summary>
    /// WebSocket目标配置
    /// </summary>
    public class WebSocketLoggerTargetOptions : LoggerTargetOptions
    {
        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; set; } = 5002;
    }
}
