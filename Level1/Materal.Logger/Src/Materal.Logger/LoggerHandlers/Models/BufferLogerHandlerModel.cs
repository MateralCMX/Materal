using Materal.Logger.Models;

namespace Materal.Logger.LoggerHandlers.Models
{
    /// <summary>
    /// 流日志处理器模型
    /// </summary>
    public abstract class BufferLogerHandlerModel : LoggerHandlerModel
    {
        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsOK { get; protected set; } = true;
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
        /// <param name="rule"></param>
        /// <param name="target"></param>
        /// <param name="model"></param>
        public BufferLogerHandlerModel(LoggerRuleConfigModel rule, LoggerTargetConfigModel target, LoggerHandlerModel model)
        {
            model.CopyProperties(this);
            Rule = rule;
            Target = target;
        }
    }
}
