using Materal.Logger.LoggerHandlers.Models;

namespace Materal.Logger.LoggerHandlers
{
    /// <summary>
    /// 日志处理器
    /// </summary>
    public interface ILoggerHandler
    {
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loggerConfig"></param>
        /// <param name="loggerLog"></param>
        void Handler(LoggerHandlerModel model, LoggerConfig loggerConfig, LoggerLog loggerLog);
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="loggerLog"></param>
        Task ShutdownAsync(LoggerLog loggerLog);
    }
}
