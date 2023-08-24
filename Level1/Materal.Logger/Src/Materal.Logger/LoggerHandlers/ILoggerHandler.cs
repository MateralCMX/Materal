using Materal.Logger.Models;

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
        void Handler(LoggerHandlerModel model);
        /// <summary>
        /// 关闭
        /// </summary>
        Task ShutdownAsync();
    }
}
