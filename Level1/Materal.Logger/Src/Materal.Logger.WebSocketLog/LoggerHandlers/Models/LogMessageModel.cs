//using Materal.Logger.LoggerHandlers;
//using Materal.Logger.LoggerHandlers.Models;
//using Materal.Logger.Models;
//using Microsoft.Extensions.Logging;

//namespace Materal.Logger.WebSocketLog.LoggerHandlers.Models
//{
//    /// <summary>
//    /// 日志消息模型
//    /// </summary>
//    public class LogMessageModel
//    {
//        /// <summary>
//        /// 控制台显示颜色
//        /// </summary>
//        public ConsoleColor Color { get; set; }
//        /// <summary>
//        /// 日志等级
//        /// </summary>
//        public LogLevel LogLevel { get; set; }
//        /// <summary>
//        /// 消息
//        /// </summary>
//        public string Message { get; set; }
//        /// <summary>
//        /// 构造方法
//        /// </summary>
//        /// <param name="target"></param>
//        /// <param name="model"></param>
//        /// <param name="loggerConfig"></param>
//        public LogMessageModel(WebSocketLoggerTargetConfigModel target, LoggerHandlerModel model, LoggerConfig loggerConfig)
//        {
//            Message = LoggerHandlerHelper.FormatMessage(loggerConfig, target.Format, model.LogLevel, model.Message, model.CategoryName, model.Scope, model.CreateTime, model.Exception, model.ThreadID, model.ID);
//            Color = target.Colors.GetConsoleColor(model.LogLevel);
//            LogLevel = model.LogLevel;
//        }
//    }
//}
