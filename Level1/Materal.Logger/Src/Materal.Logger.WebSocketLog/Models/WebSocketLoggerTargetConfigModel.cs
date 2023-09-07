using Materal.Logger.LoggerHandlers;

namespace Materal.Logger.Models
{
    /// <summary>
    /// WebSocket日志目标配置模型
    /// </summary>
    public class WebSocketLoggerTargetConfigModel : LoggerTargetConfigModel
    {
        /// <summary>
        /// 类型
        /// </summary>
        public override string Type => "WebSocket";
        /// <summary>
        /// 获得日志处理器
        /// </summary>
        public override ILoggerHandler GetLoggerHandler() => new WebSocketLoggerHandler();
        /// <summary>
        /// 日志服务准备就绪
        /// </summary>
        public override void OnLoggerServiceReady() => WebSocketLoggerHandler.RunWebSocketServer();
        private int _port = 5002;
        /// <summary>
        /// 端口号
        /// </summary>
        public int Port
        {
            get => _port;
            set
            {
                if (_port <= 0) throw new LoggerException("端口号必须大于0");
                _port = value;
            }
        }
        private string _format = "${DateTime}|${Level}|${CategoryName}|${Scope}\r\n${Message}\r\n${Exception}";
        /// <summary>
        /// 格式化
        /// </summary>
        public string Format
        {
            get => _format;
            set
            {
                if (value is null || string.IsNullOrWhiteSpace(value)) throw new LoggerException("格式化字符串不能为空");
                _format = value;
            }
        }
        /// <summary>
        /// 颜色组
        /// </summary>
        public LoggerColorsConfigModel Colors { get; set; } = new();
    }
}
