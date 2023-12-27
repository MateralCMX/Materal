namespace Materal.Logger.WebSocketLogger
{
    /// <summary>
    /// WebSocket目标配置
    /// </summary>
    public class WebSocketLoggerTargetConfig : BatchTargetConfig<WebSocketLoggerWriter>
    {
        /// <summary>
        /// 类型
        /// </summary>
        public override string Type => "WebSocket";
        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; set; } = 5002;
    }
}
