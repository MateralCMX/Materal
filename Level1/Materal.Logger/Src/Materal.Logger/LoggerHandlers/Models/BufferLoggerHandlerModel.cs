using Materal.Logger.Models;

namespace Materal.Logger.LoggerHandlers.Models
{
    /// <summary>
    /// 流日志处理器模型
    /// </summary>
    public abstract class BufferLoggerHandlerModel : LoggerHandlerModel
    {
        /// <summary>
        /// 规则
        /// </summary>
        public LoggerRuleConfigModel Rule { get; }
        /// <summary>
        /// 目标
        /// </summary>
        public LoggerTargetConfigModel Target { get; }
        /// <summary>
        /// 构造方法
        /// </summary>
        public BufferLoggerHandlerModel(LoggerRuleConfigModel rule, LoggerTargetConfigModel target, LoggerHandlerModel model)
        {
            model.CopyProperties(this);
            Rule = rule;
            Target = target;
        }
    }
}
