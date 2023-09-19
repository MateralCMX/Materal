using Materal.Logger.LoggerHandlers.Models;

namespace Materal.Logger
{
    /// <summary>
    /// BufferLoggerHandlerModel扩展
    /// </summary>
    public static class BufferLoggerHandlerModelExtension
    {
        /// <summary>
        /// 获得日志配置对象
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        /// <exception cref="LoggerException"></exception>
        public static LoggerConfig GetLoggerConfig(this BufferLoggerHandlerModel[] models)
        {
            if (models.Length <= 0) throw new LoggerException("日志处理器模型不能为空");
            return models.First().LoggerConfig;
        }
        /// <summary>
        /// 获得日志自身日志对象
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        /// <exception cref="LoggerException"></exception>
        public static ILoggerLog GetLoggerLog(this BufferLoggerHandlerModel[] models)
        {
            if (models.Length <= 0) throw new LoggerException("日志处理器模型不能为空");
            return models.First().LoggerLog;
        }
    }
}
