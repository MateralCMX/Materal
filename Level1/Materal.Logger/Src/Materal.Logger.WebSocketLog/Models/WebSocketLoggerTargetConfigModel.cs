//using Materal.Logger.LoggerHandlers;

//namespace Materal.Logger.Models
//{
//    /// <summary>
//    /// WebSocket日志目标配置模型
//    /// </summary>
//    public class WebSocketLoggerTargetConfigModel : LoggerTargetConfigModel
//    {
//        /// <summary>
//        /// 类型
//        /// </summary>
//        public override string Type => "WebSocket";
//        /// <summary>
//        /// 获得日志处理器
//        /// </summary>
//        public override ILoggerHandler GetLoggerHandler(LoggerRuntime loggerRuntime) => new WebSocketLoggerHandler(loggerRuntime, this);
//        private int _port = 5002;
//        /// <summary>
//        /// 端口号
//        /// </summary>
//        public int Port
//        {
//            get => _port;
//            set
//            {
//                if (_port <= 0) throw new LoggerException("端口号必须大于0");
//                _port = value;
//            }
//        }
//    }
//}
