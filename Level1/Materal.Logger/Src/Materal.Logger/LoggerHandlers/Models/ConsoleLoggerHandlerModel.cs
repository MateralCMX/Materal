using Materal.Logger.Models;

namespace Materal.Logger.LoggerHandlers.Models
{
    /// <summary>
    /// 控制台日志处理器模型
    /// </summary>
    public class ConsoleLoggerHandlerModel : BufferLogerHandlerModel
    {
        /// <summary>
        /// 是否可用
        /// </summary>
        public override bool IsOK => true;
        /// <summary>
        /// 输出的消息
        /// </summary>
        public string WriteMessage { get; }
        /// <summary>
        /// 颜色
        /// </summary>
        public ConsoleColor Color { get; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="target"></param>
        /// <param name="model"></param>
        public ConsoleLoggerHandlerModel(LoggerRuleConfigModel rule, LoggerTargetConfigModel target, LoggerHandlerModel model) : base(rule, target, model)
        {
            WriteMessage = LoggerHandlerHelper.FormatMessage(target.Format, model.LogLevel, model.Message, model.CategoryName, model.Scope, model.CreateTime, model.Exception, model.ThreadID);
            Color = target.Colors.GetConsoleColor(model.LogLevel);
        }
    }
}
