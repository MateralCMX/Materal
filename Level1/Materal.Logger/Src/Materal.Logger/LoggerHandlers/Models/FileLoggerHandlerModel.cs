using Materal.Logger.Models;

namespace Materal.Logger.LoggerHandlers.Models
{
    /// <summary>
    /// 控制台日志处理器模型
    /// </summary>
    public class FileLoggerHandlerModel : BufferLogerHandlerModel
    {
        /// <summary>
        /// 输出的消息
        /// </summary>
        public string FileContent { get; } = string.Empty;
        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; } = string.Empty;
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
        public FileLoggerHandlerModel(LoggerRuleConfigModel rule, LoggerTargetConfigModel target, LoggerHandlerModel model) : base(rule, target, model)
        {
            if (target.Path is null || string.IsNullOrWhiteSpace(target.Path))
            {
                IsOK = false;
                return;
            }
            Path = LoggerHandlerHelper.FormatPath(target.Path, model.LogLevel, model.CategoryName, model.Scope, model.CreateTime, model.ThreadID);
            FileContent = LoggerHandlerHelper.FormatMessage(target.Format, model.LogLevel, model.Message, model.CategoryName, model.Scope, model.CreateTime, model.Exception, model.ThreadID);
        }
    }
}
