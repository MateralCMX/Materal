using Materal.Logger.LoggerHandlers;
using Materal.Logger.LoggerHandlers.Models;
using Materal.Logger.Models;
using Microsoft.Extensions.Logging;

namespace Materal.Logger.WebSocketLog.LoggerHandlers.Models
{
    /// <summary>
    /// 日志消息模型
    /// </summary>
    public class LogMessageModel
    {
        /// <summary>
        /// 控制台显示颜色
        /// </summary>
        public ConsoleColor Color { get; set; }
        /// <summary>
        /// 日志等级
        /// </summary>
        public LogLevel LogLevel { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="target"></param>
        /// <param name="model"></param>
        public LogMessageModel(WebSocketLoggerTargetConfigModel target, LoggerHandlerModel model)
        {
            Message = LoggerHandlerHelper.FormatMessage(target.Format, model.LogLevel, model.Message, model.CategoryName, model.Scope, model.CreateTime, model.Exception, model.ThreadID);
            Color = target.Colors.GetConsoleColor(model.LogLevel);
            LogLevel = model.LogLevel;
        }
    }
}
