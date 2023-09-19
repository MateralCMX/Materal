using Materal.Logger.LoggerHandlers;

namespace Materal.Logger.Models
{
    /// <summary>
    /// 日志目标配置模型
    /// </summary>
    public abstract class LoggerTargetConfigModel
    {
        /// <summary>
        /// 启用标识
        /// </summary>
        public bool Enable { get; set; } = true;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 类型
        /// </summary>
        public abstract string Type { get; }
        /// <summary>
        /// 获得日志处理器
        /// </summary>
        public abstract ILoggerHandler GetLoggerHandler();
        /// <summary>
        /// 日志服务准备就绪
        /// </summary>
        public virtual void OnLoggerServiceReady() { }
    }
}
