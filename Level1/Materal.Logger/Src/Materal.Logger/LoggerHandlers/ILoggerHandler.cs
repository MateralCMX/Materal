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
        void Handler(LoggerHandlerModel model);
        /// <summary>
        /// 关闭
        /// </summary>
        Task ShutdownAsync();
    }
    ///// <summary>
    ///// 日志处理器
    ///// </summary>
    //public interface ILoggerHandler<T> : ILoggerHandler
    //    where T : LoggerTargetConfigModel
    //{
    //    /// <summary>
    //    /// 处理
    //    /// </summary>
    //    /// <param name="model"></param>
    //    /// <param name="rules"></param>
    //    /// <param name="targets"></param>
    //    /// <param name="defaultLogLevels"></param>
    //    void Handler(LoggerHandlerModel model, List<LoggerRuleConfigModel> rules, List<T> targets, Dictionary<string, LogLevel> defaultLogLevels);
    //}
}
